namespace MoneyHelp.DataAccess.Abstractions.Migrations;

public interface IMigrationExecutor
{
    Task<bool> Execute();
}
