using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.Abstractions.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<Result<T>> GetById(Guid id);
    Task<Result<IEnumerable<T>>> GetAll();
    Task<Result<T>> Add(T entity);
    Task<Result<T>> Update(T entity);
    Task<Result<T>> Delete(Guid id);
}
