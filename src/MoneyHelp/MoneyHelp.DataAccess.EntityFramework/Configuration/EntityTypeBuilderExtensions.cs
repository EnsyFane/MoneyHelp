using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.DataAccess.Abstractions.Extensions;

internal static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<T> AddDefaultConfigs<T>(this EntityTypeBuilder<T> builder) where T : Entity
    {
        builder.HasKey(e => e.Id)
            .IsClustered(true);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.UserId)
            .IsRequired()
            .Metadata
            .SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(p => p.CreatedOn)
            .AsUtcDateTime()
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd()
            .Metadata
            .SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(p => p.LastUpdatedOn)
            .AsUtcDateTime()
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(p => p.DeletedOn)
            .AsUtcDateTime();

        builder.HasQueryFilter(e => e.DeletedOn == null);

        return builder;
    }

    public static PropertyBuilder<DateTime> AsUtcDateTime(this PropertyBuilder<DateTime> builder)
        => builder.HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    public static PropertyBuilder<DateTime?> AsUtcDateTime(this PropertyBuilder<DateTime?> builder)
        => builder.HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);
}
