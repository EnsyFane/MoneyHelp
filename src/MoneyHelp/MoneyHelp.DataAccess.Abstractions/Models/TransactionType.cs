namespace MoneyHelp.DataAccess.Abstractions.Models;

public sealed record TransactionType : Entity
{
    public Guid UserId { get; init; }
    public required string Name { get; init; }
}
