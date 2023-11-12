using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using MoneyHelp.Core.Configuration;

namespace MoneyHelp.DataAccess.EntityFramework;

internal class MoneyHelpDbContext : DbContext
{
    private readonly DbConfig _config;

    public MoneyHelpDbContext(IOptions<DbConfig> config, DbContextOptions<MoneyHelpDbContext> options) : base(options)
    {
        _config = config.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_config.Schema);
    }
}
