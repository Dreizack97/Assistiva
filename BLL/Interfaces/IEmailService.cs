namespace BLL.Interfaces
{
    /// <summary>
    /// Define operaciones para el envío de correos electrónicos.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Envía un correo electrónico a una o más direcciones de manera asíncrona.
        /// </summary>
        /// <param name="addresses">Dirección o direcciones separadas por comas.</param>
        /// <param name="subject">Asunto del correo.</param>
        /// <param name="body">Cuerpo del correo en formato HTML.</param>
        /// <param name="attachments">Rutas de archivos adjuntos (opcional).</param>
        /// <returns><c>True</c> si el envío fue exitoso.</returns>
        /// <exception cref="SmtpCommandException">Error durante el envío.</exception>
        Task<bool> SendEmailAsync(string addresses, string subject, string body, List<string>? attachments = null);

        /// <summary>
        /// Envía un correo electrónico a una lista de direcciones de manera asíncrona.
        /// </summary>
        /// <param name="addresses">Lista de direcciones de correo.</param>
        /// <param name="subject">Asunto del correo.</param>
        /// <param name="body">Cuerpo del correo en formato HTML.</param>
        /// <param name="attachments">Rutas de archivos adjuntos (opcional).</param>
        /// <returns><c>True</c> si el envío fue exitoso.</returns>
        /// <exception cref="SmtpCommandException">Error durante el envío.</exception>
        Task<bool> SendEmailAsync(List<string> addresses, string subject, string body, List<string>? attachments = null);
    }
}
