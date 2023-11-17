using MoneyHelp.Api.Services.Abstractions;
using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;

namespace MoneyHelp.Api.Services;

internal sealed class TransactionService : ITransactionService
{
    public Task<Result<Transaction>> Add(Transaction entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Transaction>>> GetAll(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Transaction>> GetById(Guid userId, Guid id, CancellationToken ct)
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

    public Task<Result> Update(Transaction entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
