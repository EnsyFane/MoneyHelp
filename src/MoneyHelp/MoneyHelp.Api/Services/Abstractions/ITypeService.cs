using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;

namespace MoneyHelp.Api.Services.Abstractions;

internal interface ITypeService
{
    Task<Result<TransactionType>> GetById(Guid userId, Guid id, CancellationToken ct);
    Task<Result<IEnumerable<TransactionType>>> GetAll(Guid userId, CancellationToken ct);
    Task<Result<TransactionType>> Add(TransactionType entity, CancellationToken ct);
    Task<Result> Update(TransactionType entity, CancellationToken ct);
    Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct);
    Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct);
}
