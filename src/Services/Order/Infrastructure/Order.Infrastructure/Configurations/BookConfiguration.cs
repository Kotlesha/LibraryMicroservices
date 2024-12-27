using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Infrastructure.Constants;
using Shared.CleanArchitecture.Infrastructure.Configurations;

namespace Order.Infrastructure.Configurations;

internal class BookConfiguration : BaseEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Book);

        builder
           .Property(b => b.Title)
           .IsRequired();

        builder
           .Property(b => b.Price)
           .IsRequired();

        builder
           .Property(b => b.IsAvailable)
           .IsRequired();
    }
}
