namespace MoneyHelp.DataAccess.Abstractions.Models;

public sealed record TransactionType : Entity
{
    public required string Name { get; init; }
}
