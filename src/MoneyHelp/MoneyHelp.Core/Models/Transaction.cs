namespace MoneyHelp.Core.Models;

public sealed record Transaction
{
    public Guid Id { get; init; }

    public Guid WalletId { get; init; }
    public Guid UserId { get; init; }
    public Guid TypeId { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public DateTime Timestamp { get; init; }

    public DateTime CreatedOn { get; init; }
    public DateTime? LastUpdatedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
}
