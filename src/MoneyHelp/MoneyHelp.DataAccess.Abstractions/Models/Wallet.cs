namespace MoneyHelp.DataAccess.Abstractions.Models;

public sealed record Wallet : Entity
{
    public Guid UserId { get; init; }
    public required string Name { get; init; }
}
