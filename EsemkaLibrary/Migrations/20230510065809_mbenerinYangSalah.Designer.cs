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
    [Migration("20230510065809_mbenerinYangSalah")]
    partial class mbenerinYangSalah
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

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LasttName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookInformationIsbnId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsTadon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShelfCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookInformationIsbnId");

                    b.ToTable("Books");
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

            modelBuilder.Entity("EsemkaLibrary.Model.Borrow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookInformationIsbnId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BorrowAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CodeBookId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DueAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmailUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookInformationIsbnId");

                    b.HasIndex("CodeBookId");

                    b.HasIndex("EmailUserId");

                    b.ToTable("Borrows");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.Book", b =>
                {
                    b.HasOne("EsemkaLibrary.Model.BookInformation", "BookInformationIsbn")
                        .WithMany("Books")
                        .HasForeignKey("BookInformationIsbnId");

                    b.Navigation("BookInformationIsbn");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.Borrow", b =>
                {
                    b.HasOne("EsemkaLibrary.Model.BookInformation", "BookInformationIsbn")
                        .WithMany()
                        .HasForeignKey("BookInformationIsbnId");

                    b.HasOne("EsemkaLibrary.Model.Book", "CodeBook")
                        .WithMany()
                        .HasForeignKey("CodeBookId");

                    b.HasOne("EsemkaLibrary.Model.User", "EmailUser")
                        .WithMany()
                        .HasForeignKey("EmailUserId");

                    b.Navigation("BookInformationIsbn");

                    b.Navigation("CodeBook");

                    b.Navigation("EmailUser");
                });

            modelBuilder.Entity("EsemkaLibrary.Model.BookInformation", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
