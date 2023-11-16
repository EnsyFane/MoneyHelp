using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MoneyHelp.DataAccess.Abstractions.Migrations;

namespace MoneyHelp.DataAccess.EntityFramework.Migrations;

internal sealed class MigrationExecutor : IMigrationExecutor
{
    private readonly IEnumerable<DbContext> _dbContexts;
    private readonly ILogger<MigrationExecutor> _logger;

    public MigrationExecutor(IEnumerable<DbContext> dbContexts, ILogger<MigrationExecutor> logger)
    {
        _dbContexts = dbContexts ?? throw new ArgumentNullException(nameof(dbContexts));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Execute()
    {
        _logger.LogInformation("Starting migrations...");
        try
        {
            foreach (var dbContext in _dbContexts)
            {
                var connectionString = dbContext.Database.GetConnectionString()!;
                var useOnlineIndex = await GetSupportsOnlineIndex(connectionString);
                _logger.LogInformation("Using online index: {useOnlineIndex}", useOnlineIndex);

                var migrations = dbContext.Database.GetPendingMigrations().ToList();
                if (migrations.Any())
                {
                    _logger.LogInformation("Applying {migrationCount} migrations.", migrations.Count);
                    await dbContext.Database.MigrateAsync();
                }
                else
                {
                    _logger.LogWarning("No migrations to apply for DbContext: {dbContext}.", dbContext.GetType().Name);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Migrations failed: Exception: {exception}", ex);
            return false;
        }

        return true;
    }

    private async Task<bool> GetSupportsOnlineIndex(string connectionString)
    {
        try
        {
            using var sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            using var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT IIF(SERVERPROPERTY('EngineEdition') IN (3, 5, 8, 9), 1, 0)";
            var result = await sqlCommand.ExecuteScalarAsync();
            return (int)result! == 1;
        }
        catch(Exception ex)
        {
            _logger.LogWarning("Could not determine if online index is supported. Defaulting to false. Exception: {exception}", ex);
            return false;
        }
    }
}
