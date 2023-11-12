using DB = MoneyHelp.DataAccess.Abstractions.Models;
using Domain = MoneyHelp.Core.Models;

namespace MoneyHelp.DataAccess.Abstractions.Extensions;

public static class Mapper
{
    public static Domain.Transaction ToDomain(this DB.Transaction transaction)
        => new()
        {
            Id = transaction.Id,
            WalletId = transaction.WalletId,
            UserId = transaction.UserId,
            TypeId = transaction.TypeId,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Timestamp = transaction.Timestamp,
            CreatedOn = transaction.CreatedOn,
            LastUpdatedOn = transaction.LastUpdatedOn,
            DeletedOn = transaction.DeletedOn
        };

    public static Domain.TransactionType ToDomain(this DB.TransactionType transactionType)
        => new()
        {
            Id = transactionType.Id,
            Name = transactionType.Name,
            CreatedOn = transactionType.CreatedOn,
            LastUpdatedOn = transactionType.LastUpdatedOn,
            DeletedOn = transactionType.DeletedOn
        };

    public static Domain.Wallet ToDomain(this DB.Wallet wallet)
        => new()
        {
            Id = wallet.Id,
            UserId = wallet.UserId,
            Name = wallet.Name,
            CreatedOn = wallet.CreatedOn,
            LastUpdatedOn = wallet.LastUpdatedOn,
            DeletedOn = wallet.DeletedOn
        };

}
