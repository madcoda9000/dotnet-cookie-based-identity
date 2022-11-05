using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.Models.Identity
{
    /// <summary>
    /// class to define a role object
    /// </summary>
    public class AppRole : IdentityRole
    {
        /// <summary>
        /// property created on
        /// </summary>
        /// <value>DateTime</value>
        public DateTime CreatedOn { get; set; }
    }
}