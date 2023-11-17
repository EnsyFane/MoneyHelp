using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MoneyHelp.Core;
using MoneyHelp.DataAccess.Abstractions.Extensions;
using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.EntityFramework.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public const string TableName = "Transactions";

    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(TableName);
        builder.AddDefaultConfigs();

        builder.Property(x => x.WalletId)
            .IsRequired();

        builder.Property(x => x.TypeId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("money");

        builder.Property(x => x.Description)
            .HasMaxLength(Constants.MAX_TRANSACTION_DESCRIPTION_LENGTH);

        builder.Property(x => x.Timestamp)
            .IsRequired()
            .AsUtcDateTime();
    }
}
