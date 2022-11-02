using System.Net;
using System.Net.Mail;
using DotNetIdentity.Models.BusinessModels;
using Microsoft.Extensions.Options;

namespace DotNetIdentity.Helpers
{
    public class EmailHelper
    {
        private readonly EmailSettings _settings;

        public EmailHelper(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(EmailModel model)
        {
            var mail = new MailMessage
            {
                Subject = model.Subject,
                Body = model.Body,
                From = new MailAddress(_settings.UserName),
                IsBodyHtml = true
            };

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.UserName, _settings.Password)
            };

            mail.To.Add(model.To);

            await smtp.SendMailAsync(mail);
        }
    }
}