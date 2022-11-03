using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Models.BusinessModels {
    public class GlobalSettings : AppSettingsBase{
        public string? ApplicationName {get; set;}
        public string? SessionTimeoutWarnAfter {get; set;}
        public string? SessionTimeoutRedirAfter {get; set;}
        public string? SessionCookieExpiration {get; set;}
    }
}