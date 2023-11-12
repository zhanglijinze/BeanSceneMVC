﻿// <auto-generated />
using System;
using BeanSceneMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeanSceneMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231112034336_add-migration AddApplicationUser")]
    partial class addmigrationAddApplicationUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeanSceneMVC.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BeanSceneMVC.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Main"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Outside"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Balcony"
                        });
                });

            modelBuilder.Entity("BeanSceneMVC.Models.MenuCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("MenuCategories");
                });

            modelBuilder.Entity("BeanSceneMVC.Models.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsAllergenFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDiaryFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGlutenFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVegan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("bit");

                    b.Property<int>("MenuCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("MenuCategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("BeanSceneMVC.Models.Sitting", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("SittingTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTimeId")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTimeId")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Date", "SittingTypeId");

                    b.HasIndex("EndTimeId");

                    b.HasIndex("SittingTypeId");

                    b.HasIndex("StartTimeId");

                    b.ToTable("Sittings");
                });

            modelBuilder.Entity("BeanSceneMVC.Models.SittingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SittingTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Breakfast"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Lunch"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dinner"
                        });
                });

            modelBuilder.Entity("BeanSceneMVC.Models.Table", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.HasIndex("AreaId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            Code = "M1",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M2",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M3",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M4",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M5",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M6",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M7",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M8",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M9",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "M10",
                            AreaId = 1
                        },
                        new
                        {
                            Code = "O1",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O2",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O3",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O4",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O5",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O6",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O7",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O8",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O9",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "O10",
                            AreaId = 2
                        },
                        new
                        {
                            Code = "B1",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B2",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B3",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B4",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B5",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B6",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B7",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B8",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B9",
                            AreaId = 3
                        },
                        new
                        {
                            Code = "B10",
                            AreaId = 3
                        });
                });

            modelBuilder.Entity("BeanSceneMVC.Models.Timeslot", b =>
                {
                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Time");

                    b.ToTable("Timeslots");

                    b.HasData(
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 8, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 9, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 11, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 12, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 13, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 15, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 15, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 16, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 18, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 19, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 20, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 20, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 21, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 21, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Time = new DateTime(2000, 1, 1, 22, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BeanSceneMVC.Models.MenuItem", b =>
                {
                    b.HasOne("BeanSceneMVC.Models.MenuCategory", "MenuCategory")
                        .WithMany()
                        .HasForeignKey("MenuCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuCategory");
                });

            modelBuilder.Entity("BeanSceneMVC.Models.Sitting", b =>
                {
                    b.HasOne("BeanSceneMVC.Models.Timeslot", "EndTime")
                        .WithMany()
                        .HasForeignKey("EndTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeanSceneMVC.Models.SittingType", "SittingType")
                        .WithMany()
                        .HasForeignKey("SittingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeanSceneMVC.Models.Timeslot", "StartTime")
                        .WithMany()
                        .HasForeignKey("StartTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndTime");

                    b.Navigation("SittingType");

                    b.Navigation("StartTime");
                });

            modelBuilder.Entity("BeanSceneMVC.Models.Table", b =>
                {
                    b.HasOne("BeanSceneMVC.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
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
                    b.HasOne("BeanSceneMVC.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BeanSceneMVC.Models.ApplicationUser", null)
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

                    b.HasOne("BeanSceneMVC.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BeanSceneMVC.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
