using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Errors;
using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal abstract class BaseRepository<T> : IRepository<T> where T : Entity
{
    protected readonly DbSet<T> _set;
    protected readonly MoneyHelpDbContext _dbContext;
    protected readonly ILogger _logger;

    protected BaseRepository(MoneyHelpDbContext dbContext, DbSet<T> set, ILogger logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _set = set ?? throw new ArgumentNullException(nameof(set));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<T>> Add(T entity, CancellationToken ct)
    {
        try
        {
            var normailzedEntity = entity with
            {
                Id = null,
                DeletedOn = null
            };

            await _set.AddAsync(normailzedEntity, ct);

            var affectedRows = await _dbContext.SaveChangesAsync(ct);
            if (affectedRows < 1)
            {
                _logger.LogError("Entity of type {type} not added to the database for user {userId}.", typeof(T).Name, entity.UserId);
                return Result.Failure<T>(new ChangesNotSavedError());
            }
            if (affectedRows > 1)
            {
                _logger.LogError("Too many rows affected when adding entity of type {type} to the database for user {userId}.", typeof(T).Name, entity.UserId);
                return Result.Failure<T>(new TooManyRowsAffectedError());
            }

            return Result.Success(normailzedEntity);
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<T>(ex);
        }
    }

    public async Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct)
    {
        try
        {
            var deleteDate = DateTime.UtcNow;

            var affectedRows = await _set.AsQueryable()
                .Where(e => e.Id == id
                    && e.UserId == userId)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.DeletedOn, deleteDate), ct);

            if (affectedRows < 1)
            {
                _logger.LogError("Entity of type {type} with id {id} not soft deleted for user {userId}.", typeof(T).Name, id, userId);
                return Result.Failure(new ChangesNotSavedError());
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<bool>(ex);
        }
    }

    public async Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct)
    {
        try
        {
            var affectedRows = await _set.AsQueryable()
                .Where(e => e.Id == id
                    && e.UserId == userId)
                .ExecuteDeleteAsync(ct);

            if (affectedRows < 1)
            {
                _logger.LogError("Entity of type {type} with id {id} not hard deleted for user {userId}.", typeof(T).Name, id, userId);
                return Result.Failure(new ChangesNotSavedError());
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<bool>(ex);
        }
    }

    public async Task<Result<IEnumerable<T>>> GetAll(Guid userId, CancellationToken ct)
    {
        try
        {
            var result = await _set.AsQueryable()
                .Where(e => e.UserId == userId)
                .ToListAsync(ct);

            return Result.Success(result.AsEnumerable());
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<IEnumerable<T>>(ex);
        }
    }

    public async Task<Result<T>> GetById(Guid userId, Guid id, CancellationToken ct)
    {
        try
        {
            var result = await _set.AsQueryable()
                .Where(e => e.Id == id
                    && e.UserId == userId)
                .FirstOrDefaultAsync(ct);

            if (result is null)
            {
                _logger.LogWarning("Entity of type {type} with id {id} not found for user {userId}.", typeof(T).Name, id, userId);
                return Result.Failure<T>(new EntityNotFoundError<T>());
            }

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<T>(ex);
        }
    }

    public async Task<Result> Update(T entity, CancellationToken ct)
    {
        try
        {
            var affectedRows = await _set.AsQueryable()
                .Where(e => e.Id == entity.Id
                    && e.UserId == entity.UserId)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p, entity), ct);

            if (affectedRows < 1)
            {
                _logger.LogError("Entity of type {type} with id {id} not updated for user {userId}.", typeof(T).Name, entity.Id, entity.UserId);
                return Result.Failure(new ChangesNotSavedError());
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<T>(ex);
        }
    }

    protected Result<TResult> HandleRepositoryException<TResult>(Exception ex)
    {
        switch (ex)
        {
            default:
                var error = new GenericRepositoryError(ex);
                _logger.LogError(error.Message);
                return Result.Failure<TResult>(error);
        }
    }
}
