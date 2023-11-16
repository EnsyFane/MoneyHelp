using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MoneyHelp.Core;
using MoneyHelp.DataAccess.Abstractions.Extensions;
using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.EntityFramework.Configuration;

public class TypeConfiguration : IEntityTypeConfiguration<TransactionType>
{
    public const string TableName = "TransactionTypes";

    public void Configure(EntityTypeBuilder<TransactionType> builder)
    {
        builder.ToTable(TableName);
        builder.AddDefaultConfigs();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(Constants.MAX_TRANSACTION_TYPE_NAME_LENGTH);
    }
}
