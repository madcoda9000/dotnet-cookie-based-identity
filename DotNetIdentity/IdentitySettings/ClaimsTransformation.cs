using System.Security.Claims;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.IdentitySettings
{
    /// <summary>
    /// class to define own claims
    /// </summary>
    public class ClaimsTransformation : IClaimsTransformation
    {
        /// <summary>
        /// UserManager property
        /// </summary>
        private readonly UserManager<AppUser> _userManager;
        /// <summary>
        /// RoleManager property
        /// </summary>
        private readonly RoleManager<AppRole> _roleManager;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="userManager">UserManager</param>
        /// <param name="roleManager">RoleManager</param>
        public ClaimsTransformation(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Task to add claims to identoty
        /// </summary>
        /// <param name="principal">ClaimsPrincipal</param>
        /// <returns>ClaimsPrincipal</returns>
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            var user = await _userManager.FindByNameAsync(identity?.Name!);
            if (user != null)
            {
                if (!principal.HasClaim(c => c.Type == ClaimTypes.Gender))
                {
                    var genderClaim = new Claim(ClaimTypes.Gender, Enum.GetName(user.Gender)!);
                    identity?.AddClaim(genderClaim);
                }

                if (!principal.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                {
                    var birthDayClaim = new Claim(ClaimTypes.DateOfBirth, user.BirthDay.ToShortDateString());
                    identity?.AddClaim(birthDayClaim);
                }

                if (!principal.HasClaim(c => c.Type == "FreeTrial"))
                {
                    var freeTrialClaim = new Claim("FreeTrial", user.CreatedOn.ToShortDateString());
                    identity?.AddClaim(freeTrialClaim);
                }

                if (!principal.HasClaim(c => c.Type == "Department") && !String.IsNullOrEmpty(user.Department))
                {
                    var departmentClaim = new Claim("Department", user.Department!);
                    identity?.AddClaim(departmentClaim);
                }

                if (!principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
                {
                    var givennameClaim = new Claim(ClaimTypes.GivenName, user.FirstName!);
                    identity?.AddClaim(givennameClaim);
                }

                if (!principal.HasClaim(c => c.Type == ClaimTypes.Surname))
                {
                    var surnameClaim = new Claim(ClaimTypes.Surname, user.LastName!);
                    identity?.AddClaim(surnameClaim);
                }

                if(!principal.HasClaim(c=>c.Type=="SelectedCulture")) {
                    var culture = System.Globalization.CultureInfo.CurrentCulture.ToString().ToLower();
                    var cultureClaim = new Claim("SelectedCulture", culture);
                    identity?.AddClaim(cultureClaim);
                }

                if (!principal.HasClaim(c => c.Type == "IsLdapUser"))
                {
                    var ldapClaim = new Claim("IsLdapUser", user.IsLdapLogin.ToString());
                    identity?.AddClaim(ldapClaim);
                }

                if (!principal.HasClaim(c => c.Type == "RolesCombined"))
                {
                    var rls = await _userManager.GetRolesAsync(user);
                    if (rls != null)
                    {
                        var rlsComb = "";
                        if(rls.Count()>0 && rls.Count()<=1) {
                            rlsComb = rls[0].ToString();
                        } else {
                            for(int i=0;i<rls.Count;i++)
                            {
                                if(i+1!=rls.Count){ rlsComb += rls[i] + ","; }
                                else { rlsComb += rls[i]; }
                            }
                        }
                        if(rlsComb.Length>0)
                        {
                            var rolesCombinedClaim = new Claim("RolesCombined", rlsComb!);
                            identity?.AddClaim(rolesCombinedClaim);
                        }
                    }
                }
            }

            return principal;
        }
    }
}