using BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace BLL.Implementation
{
    public class EmailService : IEmailService
    {
        public struct MailServer
        {
            public const string ADDRESS = "no.reply@nukabe.mx";

            public const string PASSWORD = "SbrxTo59AQ&K";

            public const string DISPLAY_NAME = "Assistiva";

            public const string HOST = "mail.nukabe.mx";

            public const int PORT = 587;

            public const bool ENABLE_SSL = true;
        }

        public async Task<bool> SendEmailAsync(string addresses, string subject, string body, List<string>? attachments = null)
        {
            return await SendEmailAsync(new List<string> { addresses }, subject, body, attachments);
        }

        public async Task<bool> SendEmailAsync(List<string> addresses, string subject, string body, List<string>? attachments = null)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(MailServer.DISPLAY_NAME, MailServer.ADDRESS));

            foreach (string address in addresses)
                message.To.Add(MailboxAddress.Parse(address));

            message.Subject = subject;

            BodyBuilder builder = new BodyBuilder { HtmlBody = body };

            if (attachments != null)
                foreach (string attachment in attachments)
                    if (!string.IsNullOrWhiteSpace(attachment))
                        builder.Attachments.Add(attachment);

            message.Body = builder.ToMessageBody();

            using (SmtpClient smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(MailServer.HOST, MailServer.PORT, MailServer.SSL);
                    await smtpClient.AuthenticateAsync(MailServer.ADDRESS, MailServer.PASSWORD);
                    await smtpClient.SendAsync(message);
                    await smtpClient.DisconnectAsync(true);
                    return true;
                }
                catch (SmtpCommandException)
                {
                    throw;
                }
            }
        }
    }
}
