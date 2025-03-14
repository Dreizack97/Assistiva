using BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación del servicio de correo electrónico usando MailKit y SMTP.
    /// </summary>
    /// <remarks>
    /// Configuración requerida en <see cref="MailServer"/>:
    /// <list type="bullet">
    /// <item>Servidor SMTP: mail.nukabe.mx</item>
    /// <item>Autenticación con usuario y contraseña</item>
    /// <item>Conexión segura vía SSL</item>
    /// </list>
    /// </remarks>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Configuración del servidor SMTP.
        /// </summary>  
        public struct MailServer
        {
            /// <summary>Dirección del remitente.</summary>
            public const string ADDRESS = "no.reply@nukabe.mx";

            /// <summary>Contraseña del remitente.</summary>
            public const string PASSWORD = "SbrxTo59AQ&K";

            /// <summary>Nombre mostrado en el correo.</summary>
            public const string DISPLAY_NAME = "Assistiva";

            /// <summary>Host del servidor SMTP.</summary>
            public const string HOST = "mail.nukabe.mx";

            /// <summary>Puerto SMTP (587 para TLS).</summary>
            public const int PORT = 587;

            /// <summary>Habilita SSL/TLS para la conexión.</summary>
            public const bool ENABLE_SSL = true;
        }

        /// <inheritdoc/>
        public async Task<bool> SendEmailAsync(string addresses, string subject, string body, List<string>? attachments = null)
        {
            return await SendEmailAsync(new List<string> { addresses }, subject, body, attachments);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para><strong>Flujo de ejecución:</strong></para>
        /// <list type="number">
        /// <item>Construye el mensaje MIME con cabeceras y cuerpo HTML.</item>
        /// <item>Adjunta archivos si se proporcionan.</item>
        /// <item>Establece conexión segura con el servidor SMTP.</item>
        /// <item>Envía el correo y cierra la conexión.</item>
        /// </list>
        /// </remarks>
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
                    await smtpClient.ConnectAsync(MailServer.HOST, MailServer.PORT, MailServer.ENABLE_SSL);
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
