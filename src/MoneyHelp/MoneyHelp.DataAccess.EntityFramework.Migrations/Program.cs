using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MoneyHelp.Core.Configuration;
using MoneyHelp.DataAccess.Abstractions.Migrations;
using MoneyHelp.DataAccess.EntityFramework;
using MoneyHelp.DataAccess.EntityFramework.Migrations;

using System.Reflection;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration(configurationBuilder =>
{
    configurationBuilder.AddJsonFile("migrationsettings.json", optional: false)
        .AddEnvironmentVariables();
}).ConfigureServices((context, services) =>
{
    services.AddOptions<DatabaseConfiguration>()
        .Bind(context.Configuration.GetRequiredSection(DatabaseConfiguration.ConfigurationName))
        .Validate(c => c.IsValid(), $"Invalid {DatabaseConfiguration.ConfigurationName} configuration.")
        .ValidateOnStart();
    var config = context.Configuration.GetRequiredSection(DatabaseConfiguration.ConfigurationName).Get<DatabaseConfiguration>()!;

    services.AddDbContext<MoneyHelpDbContext>(opts =>
    {
        opts.UseSqlServer(config.ConnectionString, sqlOpts =>
        {
            sqlOpts.EnableRetryOnFailure()
                .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, config.Schema);
        });
    });
    services.AddScoped<DbContext, MoneyHelpDbContext>()
        .AddScoped<IMigrationExecutor, MigrationExecutor>();
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
bool successful;
try
{
    var migrationExecutor = app.Services.GetRequiredService<IMigrationExecutor>();

    successful = await migrationExecutor.Execute();
}
catch (Exception ex)
{
    logger.LogCritical("Migrations failed: Exception: {exception}", ex);
    successful = false;
}

logger.LogInformation("Migrations completed: Success: {success}", successful);

if (!successful)
{
    Environment.Exit(1);
}
