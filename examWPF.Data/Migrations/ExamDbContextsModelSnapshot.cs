// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using examWPF.Data.DbContexts;

namespace examWPF.Data.Migrations
{
    [DbContext(typeof(ExamDbContexts))]
    partial class ExamDbContextsModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("examWPF.Domain.Entities.Attachments", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("examWPF.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Faculty")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("ImageId")
                        .HasColumnType("text");

                    b.Property<long?>("ImageId1")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PassportId")
                        .HasColumnType("text");

                    b.Property<long?>("PassportId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ImageId1");

                    b.HasIndex("PassportId1");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("examWPF.Domain.Entities.User", b =>
                {
                    b.HasOne("examWPF.Domain.Entities.Attachments", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId1");

                    b.HasOne("examWPF.Domain.Entities.Attachments", "Passport")
                        .WithMany()
                        .HasForeignKey("PassportId1");

                    b.Navigation("Image");

                    b.Navigation("Passport");
                });
#pragma warning restore 612, 618
        }
    }
}
