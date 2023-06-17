﻿// <auto-generated />
using System;
using Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.5.23280.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Brigade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("BrigadeNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Brigade");
                });

            modelBuilder.Entity("Domain.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("DistrictId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AuthorId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long?>("BrigadeId")
                        .HasColumnType("bigint");

                    b.Property<int>("CountDislike")
                        .HasColumnType("int");

                    b.Property<int>("CountLike")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BrigadeId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("Domain.Models.Coordinate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CityId")
                        .HasColumnType("bigint");

                    b.Property<long>("ComplaintId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DistrictId")
                        .HasColumnType("bigint");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<long?>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ComplaintId")
                        .IsUnique();

                    b.HasIndex("DistrictId");

                    b.HasIndex("RegionId");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("Domain.Models.District", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Domain.Models.Region", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.UserComplaint", b =>
                {
                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ComplaintId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Importance")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ComplaintId");

                    b.HasIndex("ComplaintId");

                    b.ToTable("UserComplaints");
                });

            modelBuilder.Entity("Domain.Models.City", b =>
                {
                    b.HasOne("Domain.Models.District", "District")
                        .WithMany("Cities")
                        .HasForeignKey("DistrictId");

                    b.HasOne("Domain.Models.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId");

                    b.Navigation("District");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.HasOne("Domain.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.Brigade", "Brigade")
                        .WithMany("Complaints")
                        .HasForeignKey("BrigadeId");

                    b.Navigation("Author");

                    b.Navigation("Brigade");
                });

            modelBuilder.Entity("Domain.Models.Coordinate", b =>
                {
                    b.HasOne("Domain.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Models.Complaint", "Complaint")
                        .WithOne("Coordinate")
                        .HasForeignKey("Domain.Models.Coordinate", "ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("City");

                    b.Navigation("Complaint");

                    b.Navigation("District");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domain.Models.District", b =>
                {
                    b.HasOne("Domain.Models.Region", "Region")
                        .WithMany("Districts")
                        .HasForeignKey("RegionId");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domain.Models.UserComplaint", b =>
                {
                    b.HasOne("Domain.Models.Complaint", "Complaint")
                        .WithMany("UserComplaints")
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Brigade", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.Navigation("Coordinate")
                        .IsRequired();

                    b.Navigation("UserComplaints");
                });

            modelBuilder.Entity("Domain.Models.District", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Domain.Models.Region", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}
