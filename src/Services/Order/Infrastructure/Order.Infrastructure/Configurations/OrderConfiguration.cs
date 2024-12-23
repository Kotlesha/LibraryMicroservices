using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Infrastructure.Constants;
using Shared.CleanArchitecture.Infrastructure.Configurations;

namespace Order.Infrastructure.Configurations;

using Order = Domain.Entities.Order;

internal class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Order);

        builder
           .Property(o => o.UserId)
           .IsRequired();

        builder
           .HasIndex(o => o.UserId);

        builder
           .Property(o => o.CreatedTimeUtc)
           .IsRequired();

        builder
           .Property(o => o.TotalCost)
           .IsRequired();

        builder
           .Property(o => o.Status)
           .IsRequired()
           .HasConversion<string>();

        builder
           .HasMany(b => b.Books)
           .WithMany(o => o.Orders);
    }
}
