using DotNetIdentity.Models.BusinessModels;

namespace DotNetIdentity.Models.ViewModels {
    /// <summary>
    /// class to define a viewmodel to update brand settings. Inheriting BrandSettings
    /// </summary>
    public class UpdateBrandSettingsViewModel : BrandSettings {
        /// <summary>
        /// property to hold the new uploaded logo image
        /// </summary>
        /// <value></value>
        public IFormFile? UploadedLogo {get;set;}
        /// <summary>
        /// property to hold the new uploaded logon background image
        /// </summary>
        /// <value></value>
        public IFormFile? UploadedLogonBackground { get; set; }
    }
}