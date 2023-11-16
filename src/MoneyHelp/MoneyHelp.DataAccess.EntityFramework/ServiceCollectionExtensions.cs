using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MoneyHelp.Core.Configuration;
using MoneyHelp.DataAccess.Abstractions.Repositories;
using MoneyHelp.DataAccess.EntityFramework.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseConfiguration>()
            .Bind(configuration.GetRequiredSection(DatabaseConfiguration.ConfigurationName))
            .Validate(c => c.IsValid(), $"Invalid {DatabaseConfiguration.ConfigurationName} configuration.")
            .ValidateOnStart();
        var config = configuration.GetRequiredSection(DatabaseConfiguration.ConfigurationName).Get<DatabaseConfiguration>()!;

        services.AddDbContext<MoneyHelpDbContext>(options =>
        {
            options.UseSqlServer(config.ConnectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }).AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IWalletRepository, WalletRepository>()
            .AddScoped<ITypeRepository, TypeRepository>()
            .AddScoped<ITransactionRepository, TransactionRepository>();
    }
}
