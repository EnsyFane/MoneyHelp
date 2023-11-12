using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Errors;
using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

using System.Runtime.CompilerServices;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal abstract class BaseRepository<T> : IRepository<T> where T : Entity
{
    private readonly DbSet<T> _set;
    private readonly MoneyHelpDbContext _dbContext;
    private readonly ILogger _logger;

    protected BaseRepository(MoneyHelpDbContext dbContext, DbSet<T> set, ILogger logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _set = set ?? throw new ArgumentNullException(nameof(set));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<T>> Add(T entity)
    {
        try
        {
            var savedEntity = entity with
            {
                Id = Guid.NewGuid(),
            };

            //await _set.AddAsync(savedEntity);
            var result = await CommitChanges();
            if (result.IsFailure)
            {
                return Result.Failure<T>(result.Error!);
            }

            return Result.Success(savedEntity);
        }
        catch (Exception ex)
        {
            var error = new GenericRepositoryError(ex);
            _logger.LogError(error.Message);
            return Result.Failure<T>(error);
        }
    }

    public Task<Result<T>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<T>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Result<T>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<T>> Update(T entity)
    {
        throw new NotImplementedException();
    }

    private async Task<Result> CommitChanges(bool shouldAffectMultipleRows = false, [CallerMemberName] string memberName = "")
    {
        var affectedRows = await _dbContext.SaveChangesAsync();
        if (affectedRows < 1)
        {
            _logger.LogWarning("Database change resulted in no affected rows. Caller method: {memberName}.", memberName);
            return Result.Failure(new ChangesNotSavedError());
        }

        if (!shouldAffectMultipleRows && affectedRows > 1)
        {
            _logger.LogError("Database change resulted in more affected rows than expected. Affected rows: {affectedRows}. Caller method: {memberName}.", affectedRows, memberName);
            return Result.Failure(new TooManyRowsAffectedError());
        }

        return Result.Success();
    }
}
