using Auth.DAL.Constants;
using Auth.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.DAL.Configurations;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable(TableNames.Account);

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(u => u.Email)
            .IsRequired();

        builder
            .Property(u => u.HashPassword)
            .IsRequired();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
