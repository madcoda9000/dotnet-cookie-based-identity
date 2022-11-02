using System;
using System.Text;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Web;

namespace DotNetIdentity.Helpers {
    public class HtmlRolesHelper  {

        public static String UserRoles(string UserId, UserManager<AppUser> umr)
        {
            
            var user = umr!.FindByIdAsync(UserId);
            var roles = umr!.GetRolesAsync(user.Result);

            var sb = new StringBuilder();

            if (roles.Result.Count() > 0)
            {
                foreach (var role in roles.Result)
                {
                    sb.Append($"<span class='badge rounded-pill bg-danger'>{role}</span>");
                }
            }
            else
            {
                sb.Append("No roles found");
            }
            return sb.ToString();
        }
    }
}