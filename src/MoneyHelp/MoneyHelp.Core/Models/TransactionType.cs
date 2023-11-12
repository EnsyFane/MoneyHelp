﻿namespace MoneyHelp.Core.Models;

public sealed record TransactionType
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public DateTime CreatedOn { get; init; }
    public DateTime? LastUpdatedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
}
