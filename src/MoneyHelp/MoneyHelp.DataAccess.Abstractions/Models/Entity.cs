namespace MoneyHelp.DataAccess.Abstractions.Models;

public abstract record Entity
{
    public Guid Id { get; init; }

    public DateTime CreatedOn { get; init; }
    public DateTime? LastUpdatedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
}
