using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetIdentity.Models.DataModels;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetIdentity.Models;

namespace DotNetIdentity.Data
{
    /// <summary>
    /// Application Database context class for mysql
    /// </summary>
    public class AppDbContextSqLite : AppDbContext
    {
        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public AppDbContextSqLite(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// override onconfiguring method
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("SqLite"));
        }

        /// <summary>
        /// Overriding OnModelCreation to seed intitial data
        /// </summary>
        /// <param name="builder">type ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationSettings>()
                    .HasKey(x => new { x.Name, x.Type });

            builder.Entity<ApplicationSettings>()
                        .Property(x => x.Value);

            // seeding default settings            
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SessionTimeoutWarnAfter", Type = "GlobalSettings", Value = "50000" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SessionTimeoutRedirAfter", Type = "GlobalSettings", Value = "70000" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SessionCookieExpiration", Type = "GlobalSettings", Value = "7" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ShowMfaEnableBanner", Type = "GlobalSettings", Value = "true" });

            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "Username", Type = "MailSettings", Value = "YOUR_Smtp_Username" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "Password", Type = "MailSettings", Value = "YOUR_SmtpPassword" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SmtpServer", Type = "MailSettings", Value = "YOUR_SmtpServer" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SmtpPort", Type = "MailSettings", Value = "587" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SmtpUseTls", Type = "MailSettings", Value = "true" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "SmtpFromAddress", Type = "MailSettings", Value = "YOUR_From_Address" });

            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "LdapDomainController", Type = "LdapSettings", Value = "YOUR_Domaincontroller_FQDN" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "LdapDomainName", Type = "LdapSettings", Value = "YOUR_Domainname" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "LdapBaseDn", Type = "LdapSettings", Value = "DC=YOUR,DC=Domain,DC=com" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "LdapGroup", Type = "LdapSettings", Value = "YOUR_Ldap_Group" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "LdapEnabled", Type = "LdapSettings", Value = "true" });

            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ApplicationName", Type = "BrandSettings", Value = "YourApp" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorPrimary", Type = "BrandSettings", Value = "#090251" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorSecondary", Type = "BrandSettings", Value = "#f5023c" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorInfo", Type = "BrandSettings", Value = "#2196f3" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorSuccess", Type = "BrandSettings", Value = "#00c853" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorWarning", Type = "BrandSettings", Value = "#ff9800" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorDanger", Type = "BrandSettings", Value = "#f5023c" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorLightBackground", Type = "BrandSettings", Value = "#f2f7ff" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorLink", Type = "BrandSettings", Value = "#f5023c" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorHeadlines", Type = "BrandSettings", Value = "#090251" });
            builder.Entity<ApplicationSettings>().HasData(new ApplicationSettings { Name = "ColorTextMuted", Type = "BrandSettings", Value = "#a0aabb" });

            //Seeding roles to AspNetRoles table
            builder.Entity<AppRole>().HasData(new AppRole { Id = "dffc6dd5-b145-41e9-a861-c87ff673e9ca", Name = "Admin", NormalizedName = "ADMIN".ToUpper() });
            builder.Entity<AppRole>().HasData(new AppRole { Id = "f8a527ac-d7f6-4d9d-aca6-46b2261b042b", Name = "User", NormalizedName = "USER".ToUpper() });
            builder.Entity<AppRole>().HasData(new AppRole { Id = "g7a527ac-d7t6-4d7z-aca6-45t2261b042b", Name = "Editor", NormalizedName = "EDITOR".ToUpper() });
            builder.Entity<AppRole>().HasData(new AppRole { Id = "p9a527ac-d77w-4d3r-aca6-35b2261b042b", Name = "Moderator", NormalizedName = "MODERATOR".ToUpper() });



            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<AppUser>();

            //Seeding the Admin User to AspNetUsers table
            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "6fbfb682-568c-4f5b-a298-85937ca4f7f3", // primary key
                    UserName = "super.admin",
                    NormalizedUserName = "SUPER.ADMIN",
                    PasswordHash = hasher.HashPassword(null!, "Test1000!"),
                    Email = "super.admin@local.app",
                    NormalizedEmail = "SUPER.ADMIN@LOCAL.APP",
                    EmailConfirmed = true,
                    FirstName = "Super",
                    LastName = "Admin",
                    IsMfaForce = false,
                    IsLdapLogin = false,
                    IsEnabled = true,
                    RolesCombined = "Admin",
                    PhoneNumber = "111",
                    Gender = Gender.Unknown
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                    UserId = "6fbfb682-568c-4f5b-a298-85937ca4f7f3"
                }
            );
        }
    }
}