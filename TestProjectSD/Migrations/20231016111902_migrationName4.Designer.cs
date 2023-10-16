﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestProjectSD;

#nullable disable

namespace TestProjectSD_withDatabase.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20231016111902_migrationName4")]
    partial class migrationName4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestProjectSD.Models.Auth", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RowId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("RowId");

                    b.ToTable("Auth");
                });

            modelBuilder.Entity("TestProjectSD.Models.BusinessLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerNumber");

                    b.ToTable("BusinessLocations");
                });

            modelBuilder.Entity("TestProjectSD.Models.Customer", b =>
                {
                    b.Property<int>("CustomerNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerNumber"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerNumber");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TestProjectSD.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerNumber")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerNumber");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TestProjectSD_withDatabase.Models.EmployeeBusnessLocation", b =>
                {
                    b.Property<int>("BusinessLocationId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("BusinessLocationId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeBusnessLocation");
                });

            modelBuilder.Entity("TestProjectSD.Models.BusinessLocation", b =>
                {
                    b.HasOne("TestProjectSD.Models.Customer", "Customer")
                        .WithMany("BusinessLocations")
                        .HasForeignKey("CustomerNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TestProjectSD.Models.Employee", b =>
                {
                    b.HasOne("TestProjectSD.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerNumber");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TestProjectSD_withDatabase.Models.EmployeeBusnessLocation", b =>
                {
                    b.HasOne("TestProjectSD.Models.BusinessLocation", null)
                        .WithMany()
                        .HasForeignKey("BusinessLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestProjectSD.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestProjectSD.Models.Customer", b =>
                {
                    b.Navigation("BusinessLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
