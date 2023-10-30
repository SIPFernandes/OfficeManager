using System.Collections;

namespace EmailSenderClient.Shared.Areas.Services
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
        public Task SendMultipleEmailAsync(IEnumerable emails, string subject, string htmlMessage);
    }
}
