using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Models.BusinessModels {
    public class MailSettings : AppSettingsBase{
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? SmtpServer { get; set; }
        public string? SmtpPort { get; set; }
        public string? SmtpFromAddress {get; set;}
        public bool SmtpUseTls { get; set; }
    }
}