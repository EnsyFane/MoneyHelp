namespace MoneyHelp.DataAccess.Abstractions.Models;

public sealed record Transaction : Entity
{
    public Guid WalletId { get; init; }
    public Guid TypeId { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public DateTime Timestamp { get; init; }
}
