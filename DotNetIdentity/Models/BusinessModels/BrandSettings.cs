using DotNetIdentity.Services.SettingsService;
namespace DotNetIdentity.Models.BusinessModels {
    /// <summary>
    /// class to define Brand settings properties
    /// </summary>
    public class BrandSettings : AppSettingsBase {
        /// <summary>
        /// property ApplicationName
        /// </summary>
        /// <value>string</value>
        public string? ApplicationName { get; set; }
        /// <summary>
        /// primary color property
        /// </summary>
        /// <value></value>
        public string? ColorPrimary {get;set;}
        /// <summary>
        /// secondary color property
        /// </summary>
        /// <value></value>
        public string? ColorSecondary { get; set; }
        /// <summary>
        /// Info color property
        /// </summary>
        /// <value></value>
        public string? ColorInfo {get;set;}
        /// <summary>
        /// Success color property
        /// </summary>
        /// <value></value>
        public string? ColorSuccess {get;set;}
        /// <summary>
        /// Warning color property
        /// </summary>
        /// <value></value>
        public string? ColorWarning {get;set;}
        /// <summary>
        /// Danger color property
        /// </summary>
        /// <value></value>
        public string? ColorDanger {get;set;}
        /// <summary>
        /// light Background color property
        /// </summary>
        /// <value></value>
        public string? ColorLightBackground {get;set;}
        /// <summary>
        /// property to sdtore the application Logo
        /// </summary>
        /// <value></value>
        public string? ApplicationLogo {get;set;}
        /// <summary>
        /// headline color property
        /// </summary>
        /// <value></value>
        public string? ColorHeadlines { get; set; }
        /// <summary>
        /// link color property
        /// </summary>
        /// <value></value>
        public string? ColorLink { get; set; }
        /// <summary>
        /// muted text color property
        /// </summary>
        /// <value></value>
        public string? ColorTextMuted { get; set; }
        /// <summary>
        /// AuthLayoutBgImage property
        /// </summary>
        /// <value></value>
        public string? LoginBackground { get; set; }
    }
}