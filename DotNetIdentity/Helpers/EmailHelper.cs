using System.Net;
using System.Net.Mail;
using DotNetIdentity.Data;
using DotNetIdentity.Models.BusinessModels;
using Microsoft.Extensions.Options;
using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Helpers
{
    /// <summary>
    /// class for email methods
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// Settings calss property
        /// </summary>
        private ISettingsService _sett;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="sett">type ISettingsService</param>
        public EmailHelper(ISettingsService sett)
        {
            _sett = sett;
        }

        /// <summary>
        /// method to send a mail
        /// </summary>
        /// <param name="model">type EmailModel</param>
        /// <returns></returns>
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