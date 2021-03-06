// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Summer.Infrastructure.MasterData;

namespace Summer.Infrastructure.MasterData.Migrations
{
    [DbContext(typeof(MasterDbContext))]
    [Migration("20211031073522_MasterDbContext_Initial")]
    partial class MasterDbContext_Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Summer.Infrastructure.MasterData.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectionString")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Host")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Host")
                        .IsUnique();

                    b.ToTable("Tenants");
                });
#pragma warning restore 612, 618
        }
    }
}
