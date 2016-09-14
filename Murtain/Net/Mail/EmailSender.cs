using Murtain.Dependency;
using Murtain.Net.Mail.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Net.Mail
{
    /// <summary>
    /// Used to send emails.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IEmailSettingConfiguration _configuration;

        /// <summary>
        /// Creates a new <see cref="SmtpEmailSender"/>.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public EmailSender(IEmailSettingConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(_configuration.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(_configuration.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        public async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        public async Task SendAsync(MailMessage mail)
        {
            await SendEmailAsync(mail);
        }

        public void Send(MailMessage mail)
        {
            SendEmail(mail);
        }

        protected async Task SendEmailAsync(MailMessage mail)
        {
            try
            {
                NormalizeMail(mail);
                using (var smtpClient = BuildSmtpClient())
                {
                    await smtpClient.SendMailAsync(mail);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        protected void SendEmail(MailMessage mail)
        {
            try
            {
                NormalizeMail(mail);
                using (var smtpClient = BuildSmtpClient())
                {
                    smtpClient.Send(mail);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public SmtpClient BuildSmtpClient()
        {
            var host = _configuration.Host;
            var port = _configuration.Port;

            var smtpClient = new SmtpClient(host, port);
            smtpClient.Timeout = 30 * 1000;
            try
            {
                if (_configuration.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (_configuration.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;
                    if (!string.IsNullOrEmpty(_configuration.UserName))
                    {
                        smtpClient.Credentials = !string.IsNullOrEmpty(_configuration.Domain)
                            ? new NetworkCredential(_configuration.UserName, _configuration.Password, _configuration.Domain)
                            : new NetworkCredential(_configuration.UserName, _configuration.Password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }


        private void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || string.IsNullOrEmpty(mail.From.Address))
            {
                mail.From = new MailAddress(
                    _configuration.DefaultFromAddress,
                    _configuration.DefaultFromDisplayName,
                    Encoding.UTF8
                    );
            }

            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }

            mail.Headers.Add("X-Priority", "3");
            mail.Headers.Add("X-MSMail-Priority", "Normal");
            mail.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");          
            mail.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
            mail.Headers.Add("ReturnReceipt", "1");
        }
    }
}
