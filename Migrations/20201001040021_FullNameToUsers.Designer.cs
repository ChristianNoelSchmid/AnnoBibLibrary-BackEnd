﻿// <auto-generated />
using System;
using AnnoBibLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnnoBibLibrary.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201001040021_FullNameToUsers")]
    partial class FullNameToUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnnoBibLibrary.Models.Annotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("QuoteData")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Annotations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Notes = "Fellowship",
                            QuoteData = "[]",
                            SourceId = 1
                        },
                        new
                        {
                            Id = 2,
                            Notes = "Towers",
                            QuoteData = "[]",
                            SourceId = 2
                        },
                        new
                        {
                            Id = 3,
                            Notes = "King",
                            QuoteData = "[]",
                            SourceId = 3
                        },
                        new
                        {
                            Id = 4,
                            Notes = "Lion",
                            QuoteData = "[]",
                            SourceId = 4
                        },
                        new
                        {
                            Id = 5,
                            Notes = "Prince",
                            QuoteData = "[]",
                            SourceId = 5
                        },
                        new
                        {
                            Id = 6,
                            Notes = "Magician",
                            QuoteData = "[]",
                            SourceId = 6
                        },
                        new
                        {
                            Id = 7,
                            Notes = "Hebrew",
                            QuoteData = "[]",
                            SourceId = 7
                        });
                });

            modelBuilder.Entity("AnnoBibLibrary.Models.AnnotationLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnnotationId")
                        .HasColumnType("int");

                    b.Property<string>("KeywordValues")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AnnotationLinks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnnotationId = 1,
                            KeywordValues = "",
                            LibraryId = 1
                        },
                        new
                        {
                            Id = 2,
                            AnnotationId = 2,
                            KeywordValues = "",
                            LibraryId = 1
                        },
                        new
                        {
                            Id = 3,
                            AnnotationId = 3,
                            KeywordValues = "",
                            LibraryId = 1
                        },
                        new
                        {
                            Id = 4,
                            AnnotationId = 4,
                            KeywordValues = "",
                            LibraryId = 2
                        },
                        new
                        {
                            Id = 5,
                            AnnotationId = 5,
                            KeywordValues = "",
                            LibraryId = 2
                        },
                        new
                        {
                            Id = 6,
                            AnnotationId = 6,
                            KeywordValues = "",
                            LibraryId = 2
                        },
                        new
                        {
                            Id = 7,
                            AnnotationId = 7,
                            KeywordValues = "",
                            LibraryId = 2
                        });
                });

            modelBuilder.Entity("AnnoBibLibrary.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AnnoBibLibrary.Models.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(511) CHARACTER SET utf8mb4")
                        .HasMaxLength(511);

                    b.Property<string>("KeywordGroups")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Libraries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The collected works of J.R.R. Tolkien, a 20th century fantasy writer.",
                            KeywordGroups = "['Places', 'People', 'Concepts']",
                            Title = "J.R.R. Tolkien Library"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The collected works of C.S. Lewis, a 20th century fantasy/science-fiction/non-fiction writer.",
                            KeywordGroups = "['Places', 'People', 'Concepts']",
                            Title = "C.S. Lewis Library"
                        });
                });

            modelBuilder.Entity("AnnoBibLibrary.Models.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Fields")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Sources");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Fields = "<<title:0>>The Lord of the Rings: the Fellowship of the Ring<<author:1>>J.R.R. Tolkien",
                            Type = "Simple"
                        },
                        new
                        {
                            Id = 2,
                            Fields = "<<title:0>>The Lord of the Rings: the Two Towers<<author:1>>J.R.R. Tolkien",
                            Type = "Simple"
                        },
                        new
                        {
                            Id = 3,
                            Fields = "<<title:0>>The Lord of the Rings: the Return of the King<<author:1>>J.R.R. Tolkien",
                            Type = "Simple"
                        },
                        new
                        {
                            Id = 4,
                            Fields = "<<title:0>>The Chronicles of Narnia: the Lion, the Witch, and the Wardrobe<<author:1>>C.S. Lewis",
                            Type = "Simple"
                        },
                        new
                        {
                            Id = 5,
                            Fields = "<<title:0>>The Chronicles of Narnia: Prince Caspian<<author:1>>C.S. Lewis",
                            Type = "Simple"
                        },
                        new
                        {
                            Id = 6,
                            Fields = "<<title:0>>The Chronicles of Narnia: the Magician's Nephew<<author:1>>C.S. Lewis",
                            Type = "Simple"
                        },
                        new
                        {
                            Id = 7,
                            Fields = "<<title:0>>Learning Biblical Hebrew<<author:1>>Karl Kutz;;Rebekah Jospberger",
                            Type = "Simple"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AnnoBibLibrary.Models.Annotation", b =>
                {
                    b.HasOne("AnnoBibLibrary.Models.Source", "Source")
                        .WithMany("Annotations")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AnnoBibLibrary.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AnnoBibLibrary.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnnoBibLibrary.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AnnoBibLibrary.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
