using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.Abstractions.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<Result<T>> GetById(Guid userId, Guid id, CancellationToken ct);
    Task<Result<IEnumerable<T>>> GetAll(Guid userId, CancellationToken ct);
    Task<Result<T>> Add(T entity, CancellationToken ct);
    Task<Result> Update(T entity, CancellationToken ct);
    Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct);
    Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct);
}
