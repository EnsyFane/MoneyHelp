using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Errors;
using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal sealed class TypeRepository : BaseRepository<TransactionType>, ITypeRepository
{
    public TypeRepository(MoneyHelpDbContext dbContext, ILogger<TypeRepository> logger) : base(dbContext, dbContext.Set<TransactionType>(), logger)
    {
    }

    public async Task<Result<TransactionType>> GetByNameExact(Guid userId, string name, CancellationToken ct)
    {
        try
        {
            var result = await _set.AsQueryable()
                .Where(e => e.UserId == userId
                    && e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync(ct);

            if (result is null)
            {
                _logger.LogWarning("Could not find transaction type with name {name} for user {userId}.", name, userId);
                return Result.Failure<TransactionType>(new EntityNotFoundError<TransactionType>());
            }

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return HandleRepositoryException<TransactionType>(ex);
        }
    }
}
