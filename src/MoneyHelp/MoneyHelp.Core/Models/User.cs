namespace MoneyHelp.Core.Models;

public sealed record User
{
    public Guid Id { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string Email { get; init; }
    public DateTime LastActivity { get; init; }

    public DateTime CreatedOn { get; init; }
    public DateTime? LastUpdatedOn { get; init; }
    public DateTime? DeletedOn { get; init; }    
}
