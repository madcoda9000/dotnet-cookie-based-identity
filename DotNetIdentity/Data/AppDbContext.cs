using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetIdentity.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {        
        }
        public DbSet<AppLogs>? AppLogs {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            

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
                    PasswordHash = hasher.HashPassword(null, "Test1000!"),
                    Email = "super.admin@local.app",
                    NormalizedEmail = "SUPER.ADMIN@LOCAL.APP",
                    EmailConfirmed = true,
                    FirstName = "Super",
                    LastName = "Admin",
                    IsMfaForce = false,
                    IsLdapLogin = false,
                    IsEnabled = true
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