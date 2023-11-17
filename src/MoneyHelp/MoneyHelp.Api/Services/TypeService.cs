using MoneyHelp.Api.Services.Abstractions;
using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;

namespace MoneyHelp.Api.Services;

internal sealed class TypeService : ITypeService
{
    public Task<Result<TransactionType>> Add(TransactionType entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<TransactionType>>> GetAll(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<TransactionType>> GetById(Guid userId, Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Update(TransactionType entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
