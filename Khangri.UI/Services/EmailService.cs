using Khangri.Entities;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Khangri.UI.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string toAddress, string subject, string body,string cc="");
    }

    public class EmailService : IEmailService
    {

        private readonly SmtpClient _smtpClient;
        private readonly EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
            _smtpClient = new SmtpClient();
            _smtpClient.Host = _settings.SmtpServer;
            _smtpClient.Port = _settings.Port;
            var credentials = new NetworkCredential(_settings.UserName, _settings.Password);
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = credentials;
            _smtpClient.EnableSsl = _settings.UseSsl;

        }

        public async Task<string> SendEmailAsync(string toAddress, string subject, string body ,string cc)
        {
            var status = string.Empty;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toAddress);
                if(!String.IsNullOrEmpty(cc))
                {
                  mail.CC.Add(cc);
                }
                mail.From = new MailAddress(_settings.FromAddress, _settings.FromUsername);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                await _smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) { ex = ex.InnerException; }
                status = ex.Message;
            }
            return status;
        }
    }
}

