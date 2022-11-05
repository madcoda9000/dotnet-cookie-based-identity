using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Models.BusinessModels {
    /// <summary>
    /// class to define a ail settings object
    /// </summary>
    public class MailSettings : AppSettingsBase{
        /// <summary>
        /// property Username
        /// </summary>
        /// <value>string</value>
        public string? Username { get; set; }
        /// <summary>
        /// property Password
        /// </summary>
        /// <value>string</value>
        public string? Password { get; set; }
        /// <summary>
        /// property SmtpServer
        /// </summary>
        /// <value>string</value>
        public string? SmtpServer { get; set; }
        /// <summary>
        /// property SmtpPort
        /// </summary>
        /// <value>string</value>
        public string? SmtpPort { get; set; }
        /// <summary>
        /// property SmtpFromAddress
        /// </summary>
        /// <value>string</value>
        public string? SmtpFromAddress {get; set;}
        /// <summary>
        /// property SmtpUseTls
        /// </summary>
        /// <value>bool</value>
        public bool SmtpUseTls { get; set; }
    }
}