namespace MoneyHelp.Services.Models.Types;

internal sealed record TypeCreate
{
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
}
