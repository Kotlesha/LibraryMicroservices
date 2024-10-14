using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.CleanArchitecture.Domain.Entities;

namespace Shared.CleanArchitecture.Infrastructure.Configurations;

public abstract class BaseEntityConfiguration<TAggregateRoot>
    : IEntityTypeConfiguration<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    public virtual void Configure(EntityTypeBuilder<TAggregateRoot> builder)
    {
        builder
            .HasKey(ar => ar.Id);

        builder
            .Property(ar => ar.Id)
            .ValueGeneratedNever()
            .IsRequired();
    }
}
