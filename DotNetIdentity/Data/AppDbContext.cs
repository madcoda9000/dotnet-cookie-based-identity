using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetIdentity.Models.DataModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetIdentity.Data
{
    /// <summary>
    /// Application Database context class
    /// </summary>
    [NotMapped]
    public abstract class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        /// <summary>
        /// Property AppLogs
        /// </summary>
        /// <value></value>
        public DbSet<AppLogsSqLite>? AppLogsSqLite { get; set; }   
        /// <summary>
        /// Property AppLogs
        /// </summary>
        /// <value></value>
        public DbSet<AppLogs>? AppLogs { get; set; }
        /// <summary>
        /// property AppSettings
        /// </summary>
        /// <value></value>
        public DbSet<ApplicationSettings>? AppSettings { get; set; }
        /// <summary>
        /// IConfiguration property
        /// </summary>
        protected readonly IConfiguration Configuration;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="cnf">type IConfiguration</param>
        /// <returns></returns>
        public AppDbContext(IConfiguration cnf) 
        {
            Configuration = cnf;
        }        
        
    }


}