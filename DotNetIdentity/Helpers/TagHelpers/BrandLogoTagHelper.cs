using System.Text;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Helpers.TagHelpers
{
    /// <summary>
    /// html taghelper class
    /// </summary>
    [HtmlTargetElement("div", Attributes = "asp-BrandLogo")]
    public class BrandLogoTagHelper : TagHelper
    {
        /// <summary>
        /// UserManager property
        /// </summary>
        private readonly ISettingsService _sett;

        /// <summary>
        /// class contsructor
        /// </summary>
        /// <param name="sett">type ISettzingsService</param>
        public BrandLogoTagHelper(ISettingsService sett)
        {
            _sett = sett;
        }

        /// <summary>
        /// html attribute definition
        /// </summary>
        /// <value></value>
        [HtmlAttributeName("asp-BrandLogo")]
        public int imgHeight { get; set; } = default!;

        #pragma warning disable 1998

        /// <summary>
        /// override ProcessAsync
        /// </summary>
        /// <param name="context">type TagHelperContext</param>
        /// <param name="output">type TagHelperOutput</param>
        /// <returns>TagHelperOutput</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var sb = new StringBuilder();
            await _sett.Save();
            if (_sett.Brand.ApplicationLogo == null)
            {
                sb.Append($"<img src='/assets/images/logo/ylogo_colored.png' style='height:" + imgHeight + "px !important;'/>");
            }
            else
            {
                sb.Append($"<img src='" + _sett.Brand.ApplicationLogo + "' style='height:" + imgHeight + "px !important;'/>");
            }

            output.Content.SetHtmlContent(sb.ToString());
        }

        #pragma warning restore 1998
    }
}