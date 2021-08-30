using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using INDG.GRIP.Trader.Domain.Aggregates.Products;

namespace INDG.GRIP.Trader.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(1024)");

            builder
                .Property(e => e.Price)
                .IsRequired()
                .HasColumnType("integer");

            builder
                .Property<int>("_statusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            builder
                .HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey("_statusId");

            builder
                .Property(e => e.SalerUserId)
                .IsRequired();

            builder
                .HasOne(e => e.Saler)
                .WithMany()
                .HasForeignKey("SalerUserId");

            builder
                .HasOne(e => e.Buyer)
                .WithMany()
                .HasForeignKey("BuyerUserId");
        }
    }
}