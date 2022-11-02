using System.Text;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetIdentity.Helpers.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "asp-ProfileImage")]
    public class UserProfileImageTagHelper : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;

        public UserProfileImageTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("asp-ProfileImage")]
        public string UserId { get; set; } = default!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var sb = new StringBuilder();
            if(user.ProfilePicture==null) {
                sb.Append($"<img src='/assets/images/faces/user.png' height='32'/>");
            } else {
                sb.Append($"<img src='" + user.ProfilePicture + "' height='32'/>");
            }           

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}