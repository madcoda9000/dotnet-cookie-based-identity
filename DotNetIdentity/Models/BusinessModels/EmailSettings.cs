namespace DotNetIdentity.Models.BusinessModels
{
    public class EmailSettings
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string SmtpServer { get; set; } = default!;
        public string SmtpPort { get; set; } = default!;
        public bool SmtpUseTls { get; set; } = default!;
    }
}