using Microsoft.Extensions.Logging;

using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal sealed class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(MoneyHelpDbContext dbContext, ILogger<TransactionRepository> logger) : base(dbContext, dbContext.Set<Transaction>(), logger)
    {
    }
}
