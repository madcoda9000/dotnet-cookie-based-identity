using System.Text;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetIdentity.Helpers.TagHelpers
{
    /// <summary>
    /// html tag helper class
    /// </summary>
    [HtmlTargetElement("td", Attributes = "asp-roles")]
    public class UserRolesTagHelper : TagHelper
    {
        /// <summary>
        /// property UserManager
        /// </summary>
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="userManager"></param>
        public UserRolesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// attribute definition
        /// </summary>
        /// <value></value>
        [HtmlAttributeName("asp-roles")]
        public string UserId { get; set; } = default!;

        /// <summary>
        /// method to override ProcessAsync
        /// </summary>
        /// <param name="context">type TagHelperContext</param>
        /// <param name="output">type TagHelperOutput</param>
        /// <returns>TagHelperOutput</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user!);

            var sb = new StringBuilder();

            if (roles.Count() > 0)
            {
                foreach (var role in roles)
                {
                    sb.Append($"<span class='badge rounded-pill bg-danger'>{role}</span>");
                }
            }
            else
            {
                sb.Append("No roles found");
            }

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}