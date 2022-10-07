﻿// <auto-generated />
using System;
using CarRentalAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentalAPI.Migrations
{
    [DbContext(typeof(CarDbContext))]
    [Migration("20220817102339_CarSeeder-v1_1")]
    partial class CarSeederv1_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CarRentalAPI.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarBrandId")
                        .HasColumnType("int");

                    b.Property<string>("CarCondition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryOfOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Displacement")
                        .HasColumnType("int");

                    b.Property<string>("Drive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fuel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FuelUsage")
                        .HasColumnType("float");

                    b.Property<string>("Generate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfAvailableModels")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPlaces")
                        .HasColumnType("int");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<string>("PresentLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriceCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.CarAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("CarAttribute");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.CarBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarBrand");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.CarEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("CarEquipment");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Alt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CarBrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.HasIndex("CarId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.Car", b =>
                {
                    b.HasOne("CarRentalAPI.Entities.CarBrand", "CarBrand")
                        .WithMany("Cars")
                        .HasForeignKey("CarBrandId");

                    b.Navigation("CarBrand");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.CarAttribute", b =>
                {
                    b.HasOne("CarRentalAPI.Entities.Car", null)
                        .WithMany("CarAttributes")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.CarEquipment", b =>
                {
                    b.HasOne("CarRentalAPI.Entities.Car", null)
                        .WithMany("CarEquipments")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.Image", b =>
                {
                    b.HasOne("CarRentalAPI.Entities.CarBrand", null)
                        .WithMany("Images")
                        .HasForeignKey("CarBrandId");

                    b.HasOne("CarRentalAPI.Entities.Car", null)
                        .WithMany("Images")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.Car", b =>
                {
                    b.Navigation("CarAttributes");

                    b.Navigation("CarEquipments");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("CarRentalAPI.Entities.CarBrand", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
