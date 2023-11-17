using Microsoft.Extensions.Logging;

using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.DataAccess.EntityFramework.Repositories;

internal sealed class WalletRepository : BaseRepository<Wallet>, IWalletRepository
{
    public WalletRepository(MoneyHelpDbContext dbContext, ILogger<WalletRepository> logger) : base(dbContext, dbContext.Set<Wallet>(), logger)
    {
    }
}
