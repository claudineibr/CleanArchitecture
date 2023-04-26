using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.EntityConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(typeof(User).Name);
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id).HasMaxLength(40);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.Phone).HasMaxLength(20);
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.Property(c => c.DateCreated).HasColumnType("DATETIME");
            builder.Property(c => c.DateUpdated).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(c => c.DateDeleted).HasColumnType("DATETIME").IsRequired(false);
        }
    }
}

