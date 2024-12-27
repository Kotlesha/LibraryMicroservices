using Book.Domain.Constants;
using Book.Domain.Entities;
using Book.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.CleanArchitecture.Infrastructure.Configurations;

namespace Book.Infrastructure.Configurations;

internal class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder
            .ToTable(TableNames.Category);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(CategoryConstants.NameMaxLength);

        builder
            .HasMany(c => c.Books)
            .WithOne(b => b.Category)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}