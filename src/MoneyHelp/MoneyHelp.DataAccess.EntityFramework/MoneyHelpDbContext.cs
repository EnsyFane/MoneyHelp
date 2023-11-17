using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using MoneyHelp.Core.Configuration;
using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.EntityFramework.Configuration;

namespace MoneyHelp.DataAccess.EntityFramework;

public class MoneyHelpDbContext : DbContext
{
    private readonly DatabaseConfiguration _config;

    public MoneyHelpDbContext(IOptions<DatabaseConfiguration> config, DbContextOptions<MoneyHelpDbContext> options) : base(options)
    {
        _config = config.Value ?? throw new ArgumentNullException(nameof(config));

        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(_config.Schema);
        
        modelBuilder.ApplyConfiguration(new TransactionConfiguration())
            .ApplyConfiguration(new TypeConfiguration())
            .ApplyConfiguration(new WalletConfiguration());

        modelBuilder.Entity<Wallet>()
            .HasMany<Transaction>()
            .WithOne()
            .HasForeignKey(t => t.WalletId)
            .HasPrincipalKey(w => w.Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TransactionType>()
            .HasMany<Transaction>()
            .WithOne()
            .HasForeignKey(t => t.TypeId)
            .HasPrincipalKey(w => w.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
