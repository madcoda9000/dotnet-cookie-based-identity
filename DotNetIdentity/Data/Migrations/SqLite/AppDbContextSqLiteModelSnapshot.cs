﻿// <auto-generated />
using System;
using DotNetIdentity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable
#pragma warning disable 1591
namespace DotNetIdentity.Data.Migrations.SqLite
{
    [DbContext(typeof(AppDbContextSqLite))]
    partial class AppDbContextSqLiteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("DotNetIdentity.Models.DataModels.ApplicationSettings", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Name", "Type");

                    b.ToTable("AppSettings");

                    b.HasData(
                        new
                        {
                            Name = "SessionTimeoutWarnAfter",
                            Type = "GlobalSettings",
                            Value = "50000"
                        },
                        new
                        {
                            Name = "SessionTimeoutRedirAfter",
                            Type = "GlobalSettings",
                            Value = "70000"
                        },
                        new
                        {
                            Name = "SessionCookieExpiration",
                            Type = "GlobalSettings",
                            Value = "7"
                        },
                        new
                        {
                            Name = "Username",
                            Type = "MailSettings",
                            Value = "YOUR_Smtp_Username"
                        },
                        new
                        {
                            Name = "Password",
                            Type = "MailSettings",
                            Value = "YOUR_SmtpPassword"
                        },
                        new
                        {
                            Name = "SmtpServer",
                            Type = "MailSettings",
                            Value = "YOUR_SmtpServer"
                        },
                        new
                        {
                            Name = "SmtpPort",
                            Type = "MailSettings",
                            Value = "587"
                        },
                        new
                        {
                            Name = "SmtpUseTls",
                            Type = "MailSettings",
                            Value = "true"
                        },
                        new
                        {
                            Name = "SmtpFromAddress",
                            Type = "MailSettings",
                            Value = "YOUR_From_Address"
                        },
                        new
                        {
                            Name = "LdapDomainController",
                            Type = "LdapSettings",
                            Value = "YOUR_Domaincontroller_FQDN"
                        },
                        new
                        {
                            Name = "LdapDomainName",
                            Type = "LdapSettings",
                            Value = "YOUR_Domainname"
                        },
                        new
                        {
                            Name = "LdapBaseDn",
                            Type = "LdapSettings",
                            Value = "DC=YOUR,DC=Domain,DC=com"
                        },
                        new
                        {
                            Name = "LdapGroup",
                            Type = "LdapSettings",
                            Value = "YOUR_Ldap_Group"
                        },
                        new
                        {
                            Name = "LdapEnabled",
                            Type = "LdapSettings",
                            Value = "true"
                        },
                        new
                        {
                            Name = "ApplicationName",
                            Type = "BrandSettings",
                            Value = "YourApp"
                        },
                        new
                        {
                            Name = "ColorPrimary",
                            Type = "BrandSettings",
                            Value = "#090251"
                        },
                        new
                        {
                            Name = "ColorSecondary",
                            Type = "BrandSettings",
                            Value = "#f5023c"
                        },
                        new
                        {
                            Name = "ColorInfo",
                            Type = "BrandSettings",
                            Value = "#2196f3"
                        },
                        new
                        {
                            Name = "ColorSuccess",
                            Type = "BrandSettings",
                            Value = "#00c853"
                        },
                        new
                        {
                            Name = "ColorWarning",
                            Type = "BrandSettings",
                            Value = "#ff9800"
                        },
                        new
                        {
                            Name = "ColorDanger",
                            Type = "BrandSettings",
                            Value = "#f5023c"
                        },
                        new
                        {
                            Name = "ColorLightBackground",
                            Type = "BrandSettings",
                            Value = "#f2f7ff"
                        },
                        new
                        {
                            Name = "ColorLink",
                            Type = "BrandSettings",
                            Value = "#f5023c"
                        },
                        new
                        {
                            Name = "ColorHeadlines",
                            Type = "BrandSettings",
                            Value = "#090251"
                        },
                        new
                        {
                            Name = "ColorTextMuted",
                            Type = "BrandSettings",
                            Value = "#a0aabb"
                        });
                });

            modelBuilder.Entity("DotNetIdentity.Models.DataModels.AppLogs", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MessageTemplate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("AppLogs");
                });

            modelBuilder.Entity("DotNetIdentity.Models.DataModels.AppLogsSqLite", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RenderedMessage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("AppLogsSqLite");
                });

            modelBuilder.Entity("DotNetIdentity.Models.Identity.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                            ConcurrencyStamp = "334a6437-8875-4468-94cd-5d9fc1758f89",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                            ConcurrencyStamp = "329ebaf5-3726-4b3b-ade5-41567ab51b03",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                            ConcurrencyStamp = "e1ecd2a6-ab93-4fa7-97c2-9b3b6e622522",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Editor",
                            NormalizedName = "EDITOR"
                        },
                        new
                        {
                            Id = "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                            ConcurrencyStamp = "27c76457-f6c8-4d47-a1da-ba74b46dee17",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Moderator",
                            NormalizedName = "MODERATOR"
                        });
                });

            modelBuilder.Entity("DotNetIdentity.Models.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Department")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLdapLogin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMfaForce")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("TEXT");

                    b.Property<string>("RolesCombined")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TwoFactorType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                            AccessFailedCount = 0,
                            BirthDay = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "96d1103f-961a-4e8c-86af-8b2ea84baff2",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = "",
                            Email = "super.admin@local.app",
                            EmailConfirmed = true,
                            FirstName = "Super",
                            Gender = 0,
                            IsEnabled = true,
                            IsLdapLogin = false,
                            IsMfaForce = false,
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPER.ADMIN@LOCAL.APP",
                            NormalizedUserName = "SUPER.ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEO3cISTsy9QCXy0k47qdYS8EL7X1nbNSngGxXU8IFF6zUA7IldmKGSJkZINQbnqwEw==",
                            PhoneNumber = "111",
                            PhoneNumberConfirmed = false,
                            RolesCombined = "Admin",
                            SecurityStamp = "18612ebe-4b95-4e35-b06a-dc873b5eac18",
                            TwoFactorEnabled = false,
                            TwoFactorType = 0,
                            UserName = "super.admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                            RoleId = "dffc6dd5-b145-41e9-a861-c87ff673e9ca"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("DotNetIdentity.Models.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DotNetIdentity.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DotNetIdentity.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("DotNetIdentity.Models.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNetIdentity.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DotNetIdentity.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618, 1591
        }
    }
}
