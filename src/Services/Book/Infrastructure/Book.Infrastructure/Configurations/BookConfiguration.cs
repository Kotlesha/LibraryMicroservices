using Book.Domain.Constants;
using Book.Domain.Enums;
using Book.Domain.Extensions;
using Book.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.CleanArchitecture.Infrastructure.Configurations;

namespace Book.Infrastructure.Configurations;

using Book = Domain.Entities.Book;

internal class BookConfiguration : BaseEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder
            .ToTable(TableNames.Book);

        builder
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(BookConstants.TitleMaxLength);

        builder
            .Property(b => b.Description)
            .HasMaxLength(BookConstants.DescriptionMaxLength);

        builder
            .Property(b => b.Price)
            .IsRequired();

        builder
            .Property(b => b.PublicationDateUtc)
            .IsRequired();

        builder
            .Property(b => b.IsAvailable)
            .IsRequired();

        builder
            .Property(b => b.Pages)
            .IsRequired();

        builder
            .Property(b => b.AgeRating)
            .IsRequired()
            .HasConversion(
                v => v.ToFormattedString(),
                v => Enum.Parse<AgeRating>(v.Replace("+", "")) 
            )
            .HasMaxLength(BookConstants.AgeRatingMaxLength)
            .IsRequired();

        builder
            .Property(b => b.Isbn)
            .IsRequired();

        builder
            .HasIndex(b => b.Title)
            .IsUnique();
    }
}