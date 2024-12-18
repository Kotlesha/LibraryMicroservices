using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.CleanArchitecture.Infrastructure.Configurations;
using User.Domain.Constants;
using User.Infrastructure.Constants;

namespace User.Infrastructure.Configurations;

using User = Domain.Entities.User;

internal class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder
            .ToTable(TableNames.User);

        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(UserConstants.NameMaxLength);

        builder
            .Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(UserConstants.SurnameMaxLength);

        builder
            .Property(u => u.Patronymic)
            .IsRequired()
            .HasMaxLength(UserConstants.PatronymicMaxLength);

        builder
            .Property(u => u.Email)
            .IsRequired();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .HasIndex(u => u.AccountId)
            .IsUnique();
    }
}
