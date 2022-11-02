using System.Text;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetIdentity.Helpers.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "asp-roles")]
    public class UserRolesTagHelper : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRolesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("asp-roles")]
        public string UserId { get; set; } = default!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user);

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