using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;
using MoneyHelp.Services.Models.Transactions;

namespace MoneyHelp.Services.Abstractions;

internal interface ITransactionService
{
    Task<Result<Transaction>> GetById(Guid userId, Guid id, CancellationToken ct);
    Task<Result<IEnumerable<Transaction>>> GetAll(Guid userId, CancellationToken ct);

    Task<Result<Transaction>> Add(TransactionCreate transactionCreate, CancellationToken ct);

    Task<Result> Update(TransactionUpdate transactionUpdate, CancellationToken ct);

    Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct);
    Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct);
}
