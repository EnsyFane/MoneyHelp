namespace MoneyHelp.Core.Models;

public sealed record Wallet
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }
    public required string Name { get; init; }

    public DateTime CreatedOn { get; init; }
    public DateTime? LastUpdatedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
}
