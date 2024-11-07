﻿// <auto-generated />
using System;
using EsemkaLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EsemkaLibrary.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230510012112_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EsemkaLibrary.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookInformationIsbnId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LasttName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookInformationIsbnId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.BookInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Isbn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn13")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LateFee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublishedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookInformations");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.Author", b =>
                {
                    b.HasOne("EsemkaLibrary.Model.BookInformation", "BookInformationIsbn")
                        .WithMany("Authors")
                        .HasForeignKey("BookInformationIsbnId");

                    b.Navigation("BookInformationIsbn");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.BookInformation", b =>
                {
                    b.Navigation("Authors");
                });
#pragma warning restore 612, 618
        }
    }
}
