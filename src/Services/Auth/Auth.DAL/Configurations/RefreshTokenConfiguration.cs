using Auth.DAL.Constants;
using Auth.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.DAL.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder
            .ToTable(TableNames.RefreshToken);

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(u => u.Token)
            .HasMaxLength(RefreshTokenConstants.MaxLength);

        builder
            .HasIndex(u => u.Token)
            .IsUnique();

        builder
            .HasOne(rt => rt.Account)
            .WithMany()
            .HasForeignKey(rt => rt.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
