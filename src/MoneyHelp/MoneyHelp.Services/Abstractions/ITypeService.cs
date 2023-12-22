using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;
using MoneyHelp.Services.Models.Types;

namespace MoneyHelp.Services.Abstractions;

internal interface ITypeService
{
    Task<Result<TransactionType>> GetById(Guid userId, Guid id, CancellationToken ct);
    Task<Result<IEnumerable<TransactionType>>> GetAll(Guid userId, CancellationToken ct);

    Task<Result<TransactionType>> Add(TypeCreate typeCreate, CancellationToken ct);

    Task<Result> Update(TypeUpdate typeUpdate, CancellationToken ct);

    Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct);
    Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct);
}
