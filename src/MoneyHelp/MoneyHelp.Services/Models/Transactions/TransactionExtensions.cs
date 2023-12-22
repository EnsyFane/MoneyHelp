using Db = MoneyHelp.DataAccess.Abstractions.Models;
using Domain = MoneyHelp.Core.Models;

namespace MoneyHelp.Services.Models.Transactions;

internal static class TransactionExtensions
{
    public static Db.Transaction ToDbModel(this TransactionCreate transactionCreate)
        => new()
        {
            WalletId = transactionCreate.WalletId,
            UserId = transactionCreate.UserId,
            TypeId = transactionCreate.TypeId,
            Amount = transactionCreate.Amount,
            Description = transactionCreate.Description,
            Timestamp = transactionCreate.Timestamp,
        };

    public static Db.Transaction ToDbModel(this TransactionUpdate transactionUpdate)
        => new()
        {
            WalletId = transactionUpdate.WalletId,
            UserId = transactionUpdate.UserId,
            TypeId = transactionUpdate.TypeId,
            Amount = transactionUpdate.Amount,
            Description = transactionUpdate.Description,
            Timestamp = transactionUpdate.Timestamp,
        };

    public static Domain.Transaction ToDomainModel(this Db.Transaction source)
        => new()
        {
            Id = source.Id ?? throw new NullReferenceException("Id in source is null."),

            WalletId = source.WalletId,
            UserId = source.UserId,
            TypeId = source.TypeId,
            Amount = source.Amount,
            Description = source.Description,
            Timestamp = source.Timestamp,

            CreatedOn = source.CreatedOn,
            LastUpdatedOn = source.LastUpdatedOn,
            DeletedOn = source.DeletedOn,
        };
}
