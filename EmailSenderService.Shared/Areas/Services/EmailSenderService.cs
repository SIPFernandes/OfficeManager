using EmailSenderClient.Shared.Data.Consts;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Net;
using System.Net.Mail;

namespace EmailSenderClient.Shared.Areas.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtp;
        private readonly string _fromEmail;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;

            _fromEmail = _configuration.GetValue<string>(EmailConst.EmailSender);

            _smtp = new SmtpClient
            {
                Host = _configuration.GetValue<string>(EmailConst.Host),
                Port = _configuration.GetValue<int>(EmailConst.Port),
                EnableSsl = true,
                Credentials = new NetworkCredential
                {
                    UserName = _fromEmail,
                    Password = _configuration.GetValue<string>(EmailConst.EmailPassword)
                }
            };
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = CreateMailMessage(subject, htmlMessage);

            message.Bcc.Add(new MailAddress(email));

            await _smtp.SendMailAsync(message);
        }

        public async Task SendMultipleEmailAsync(IEnumerable emails, string subject, string htmlMessage)
        {
            var message = CreateMailMessage(subject, htmlMessage);

            foreach (string email in emails)
            {
                message.Bcc.Add(new MailAddress(email));
            }

            await _smtp.SendMailAsync(message);
        }

        private MailMessage CreateMailMessage(string subject, string htmlMessage)
        {
            return new MailMessage
            {
                From = new MailAddress(_fromEmail),

                Subject = subject,

                Body = htmlMessage,

                IsBodyHtml = true
            };
        }
    }
}
