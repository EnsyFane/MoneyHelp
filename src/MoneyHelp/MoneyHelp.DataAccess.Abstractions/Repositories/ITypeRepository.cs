using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.Abstractions.Repositories;

public interface ITypeRepository : IRepository<TransactionType>
{
    Task<Result<TransactionType>> GetByNameExact(Guid userId, string name, CancellationToken ct);
}
