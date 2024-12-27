using Book.Domain.Constants;
using Book.Domain.Entities;
using Book.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.CleanArchitecture.Infrastructure.Configurations;

namespace Book.Infrastructure.Configurations;

internal class GenreConfiguration : BaseEntityConfiguration<Genre>
{
    public override void Configure(EntityTypeBuilder<Genre> builder)
    {
        base.Configure(builder);

        builder
            .ToTable(TableNames.Genre);

        builder
            .Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(GenreConstants.NameMaxLength);

        builder
            .HasMany(g => g.Books)
            .WithMany(b => b.Genres);

        builder
            .HasIndex(g => g.Name)
            .IsUnique();
    }
}