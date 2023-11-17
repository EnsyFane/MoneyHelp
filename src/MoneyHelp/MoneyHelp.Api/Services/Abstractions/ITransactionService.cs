using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;

namespace MoneyHelp.Api.Services.Abstractions;

internal interface ITransactionService
{
    Task<Result<Transaction>> GetById(Guid userId, Guid id, CancellationToken ct);
    Task<Result<IEnumerable<Transaction>>> GetAll(Guid userId, CancellationToken ct);
    Task<Result<Transaction>> Add(Transaction entity, CancellationToken ct);
    Task<Result> Update(Transaction entity, CancellationToken ct);
    Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct);
    Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct);
}
