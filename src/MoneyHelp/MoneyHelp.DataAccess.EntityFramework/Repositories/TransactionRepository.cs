using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal sealed class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(MoneyHelpDbContext dbContext, DbSet<Transaction> set, ILogger<TransactionRepository> logger) : base(dbContext, set, logger)
    {
    }
}
