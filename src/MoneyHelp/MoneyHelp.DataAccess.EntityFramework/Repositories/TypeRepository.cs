using Microsoft.Extensions.Logging;

using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal sealed class TypeRepository : BaseRepository<TransactionType>, ITypeRepository
{
    public TypeRepository(MoneyHelpDbContext dbContext, ILogger<TypeRepository> logger) : base(dbContext, dbContext.Set<TransactionType>(), logger)
    {
    }
}
