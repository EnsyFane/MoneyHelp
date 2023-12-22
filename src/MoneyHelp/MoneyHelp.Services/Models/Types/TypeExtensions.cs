using Db = MoneyHelp.DataAccess.Abstractions.Models;
using Domain = MoneyHelp.Core.Models;

namespace MoneyHelp.Services.Models.Types;

internal static class TypeExtensions
{
    public static Db.TransactionType ToDbModel(this TypeCreate typeCreate)
        => new()
        {
            UserId = typeCreate.UserId,
            Name = typeCreate.Name,
        };

    public static Db.TransactionType ToDbModel(this TypeUpdate typeUpdate)
        => new()
        {
            UserId = typeUpdate.UserId,
            Name = typeUpdate.Name,
        };

    public static Domain.TransactionType ToDomainModel(this Db.TransactionType source)
        => new()
        {
            Id = source.Id ?? throw new NullReferenceException("Id in source is null."),

            Name = source.Name,
            UserId = source.UserId,

            CreatedOn = source.CreatedOn,
            LastUpdatedOn = source.LastUpdatedOn,
            DeletedOn = source.DeletedOn,
        };
}
