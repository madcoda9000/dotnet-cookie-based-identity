using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Models.BusinessModels {
    /// <summary>
    /// class to define a global settings object
    /// </summary>
    public class GlobalSettings : AppSettingsBase{
        /// <summary>
        /// property ApplicationName
        /// </summary>
        /// <value>string</value>
        public string? ApplicationName {get; set;}
        /// <summary>
        /// property SessionTimeoutWarnAfter
        /// </summary>
        /// <value>string</value>
        public string? SessionTimeoutWarnAfter {get; set;}
        /// <summary>
        /// property SessionTimeoutRedirAfter
        /// </summary>
        /// <value>string</value>
        public string? SessionTimeoutRedirAfter {get; set;}
        /// <summary>
        /// property SessionCookieExpiration
        /// </summary>
        /// <value>string</value>
        public string? SessionCookieExpiration {get; set;}
    }
}