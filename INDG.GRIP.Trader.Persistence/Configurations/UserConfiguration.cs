using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("varchar(15)");

            builder
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");
        }
    }
}