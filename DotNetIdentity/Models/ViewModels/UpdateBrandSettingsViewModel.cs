using DotNetIdentity.Models.BusinessModels;

namespace DotNetIdentity.Models.ViewModels {
    /// <summary>
    /// class to define a viewmodel to update brand settings. Inheriting BrandSettings
    /// </summary>
    public class UpdateBrandSettingsViewModel : BrandSettings {
        /// <summary>
        /// property to hole the new uploaded image
        /// </summary>
        /// <value></value>
        public IFormFile? UploadedLogo {get;set;}
    }
}