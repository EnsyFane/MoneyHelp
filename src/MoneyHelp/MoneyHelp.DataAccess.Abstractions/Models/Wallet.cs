namespace MoneyHelp.DataAccess.Abstractions.Models;

public sealed record Wallet : Entity
{
    public required string Name { get; init; }
}
