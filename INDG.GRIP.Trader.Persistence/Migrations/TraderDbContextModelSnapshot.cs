// <auto-generated />
using System;
using INDG.GRIP.Trader.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace INDG.GRIP.Trader.Persistence.Migrations
{
    [DbContext(typeof(TraderDbContext))]
    partial class TraderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("INDG.GRIP.Trader.Domain.Aggregates.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BuyerUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<Guid>("SalerUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ShippingNumber")
                        .HasColumnType("text");

                    b.Property<int>("_statusId")
                        .HasColumnType("integer")
                        .HasColumnName("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerUserId");

                    b.HasIndex("SalerUserId");

                    b.HasIndex("_statusId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("INDG.GRIP.Trader.Domain.Aggregates.Products.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("INDG.GRIP.Trader.Domain.Aggregates.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("INDG.GRIP.Trader.Domain.Aggregates.Products.Product", b =>
                {
                    b.HasOne("INDG.GRIP.Trader.Domain.Aggregates.Users.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerUserId");

                    b.HasOne("INDG.GRIP.Trader.Domain.Aggregates.Users.User", "Saler")
                        .WithMany()
                        .HasForeignKey("SalerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INDG.GRIP.Trader.Domain.Aggregates.Products.Status", "Status")
                        .WithMany()
                        .HasForeignKey("_statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Saler");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
