using System.Net;
using System.Net.Mail;
using DotNetIdentity.Data;
using DotNetIdentity.Models.BusinessModels;
using Microsoft.Extensions.Options;

namespace DotNetIdentity.Helpers
{
    public class EmailHelper
    {
        private ISettingsService _sett;

        public EmailHelper(ISettingsService sett)
        {
            _sett = sett;
        }

        public async Task SendAsync(EmailModel model)
        {
            var mail = new MailMessage
            {
                Subject = model.Subject,
                Body = model.Body,
                From = new MailAddress(_sett.Mail.SmtpFromAddress!),
                IsBodyHtml = true
            };
            
            var smtp = new SmtpClient
            {                
                Host = _sett.Mail.SmtpServer!,
                Port = Convert.ToInt32(_sett.Mail.SmtpPort),
                EnableSsl = Convert.ToBoolean(_sett.Mail.SmtpUseTls),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_sett.Mail.Username, _sett.Mail.Password)
            };

            mail.To.Add(model.To);

            await smtp.SendMailAsync(mail);
        }
    }
}