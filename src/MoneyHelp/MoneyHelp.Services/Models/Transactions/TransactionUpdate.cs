namespace MoneyHelp.Services.Models.Transactions;

internal sealed record TransactionUpdate
{
    public required Guid WalletId { get; init; }
    public required Guid UserId { get; init; }
    public required Guid TypeId { get; init; }
    public required decimal Amount { get; init; }
    public string? Description { get; init; }
    public required DateTime Timestamp { get; init; }
}
