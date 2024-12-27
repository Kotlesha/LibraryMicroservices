using Book.Domain.Constants;
using Book.Domain.Entities;
using Book.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.CleanArchitecture.Infrastructure.Configurations;

namespace Book.Infrastructure.Configurations;

internal class AuthorConfiguration : BaseEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        builder
            .ToTable(TableNames.Author);

        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(AuthorConstants.NameMaxLength);

        builder
            .Property(a => a.Surname)
            .HasMaxLength(AuthorConstants.SurnameMaxLength);

        builder
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors);
    }
}