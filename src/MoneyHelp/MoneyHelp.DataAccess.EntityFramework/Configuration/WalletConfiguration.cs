using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MoneyHelp.Core;
using MoneyHelp.DataAccess.Abstractions.Extensions;
using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.EntityFramework.Configuration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public const string TableName = "Wallets";

    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(TableName);
        builder.AddDefaultConfigs();

        builder.Property(x => x.Name)
            .HasMaxLength(Constants.MAX_WALLET_NAME_LENGTH);
    }
}
