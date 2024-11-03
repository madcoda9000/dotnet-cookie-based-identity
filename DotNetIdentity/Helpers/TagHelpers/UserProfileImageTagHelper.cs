using System.Text;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetIdentity.Helpers.TagHelpers
{
    /// <summary>
    /// html taghelper class
    /// </summary>
    [HtmlTargetElement("div", Attributes = "asp-ProfileImage")]
    public class UserProfileImageTagHelper : TagHelper
    {
        /// <summary>
        /// UserManager property
        /// </summary>
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// class contsructor
        /// </summary>
        /// <param name="userManager">type UserManager</param>
        public UserProfileImageTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// html attribute definition
        /// </summary>
        /// <value></value>
        [HtmlAttributeName("asp-ProfileImage")]
        public string UserId { get; set; } = default!;

        /// <summary>
        /// override ProcessAsync
        /// </summary>
        /// <param name="context">type TagHelperContext</param>
        /// <param name="output">type TagHelperOutput</param>
        /// <returns>TagHelperOutput</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var sb = new StringBuilder();
            if(user!.ProfilePicture==null) {
                sb.Append($"<img src='/assets/images/faces/user.png' height='32' alt='User avatar image'/>");
            } else {
                sb.Append($"<img src='" + user.ProfilePicture + "' height='32' alt='User avatar image'/>");
            }           

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}