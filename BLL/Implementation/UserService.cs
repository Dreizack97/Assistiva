using System.Reflection;
using BLL.Interfaces;
using BLL.Properties;
using BLL.Utilities;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación concreta de <see cref="IUserService"/> para gestionar usuarios.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Utiliza un repositorio genérico (<see cref="IGenericRepository{T}"/>) para operaciones de base de datos
    /// y <see cref="PasswordUtility"/> para el manejo seguro de contraseñas.
    /// </para>
    /// </remarks>
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IEmailService _emailService;
        private const int VALID_TIME = 1;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de usuarios.
        /// </summary>
        /// <param name="repository">Repositorio genérico para operaciones CRUD.</param>
        /// <param name="emailService">Servicio de envío de correos electrónicos.</param>
        public UserService(IGenericRepository<User> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        #region CRUD
        /// <inheritdoc/>
        public async Task<User> CreateAsync(User user)
        {
            // Validación de disponibilidad de nombre de usuario y correo electrónico
            if (!await IsUsernameOrEmailAvailableAsync(user.Username, user.Email))
                throw new TaskCanceledException("El nombre de usuario o correo electrónico no está disponible.");

            // Generación de contraseña segura
            string password = PasswordUtility.GeneratePassword(8);
            byte[] salt = PasswordUtility.GenerateSalt();
            byte[] encryptedPassword = PasswordUtility.EncryptPassword(salt, password);

            // Asginación de valores
            user.Salt = salt;
            user.Password = encryptedPassword;

            // Creación del usuario
            User _user = await _repository.AddAsync(user);

            if (_user.UserId == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar crear el usuario.");

            // Envío de correo electrónico de bienvenida
            string htmlContent = GetMessageText("BLL.Resources.Welcome.html");
            string messageBody = htmlContent.Replace("{Username}", _user.Username).Replace("{Password}", password);
            await _emailService.SendEmailAsync(_user.Email, "Bienvenido a Assistiva", messageBody);

            return _user;
        }

        /// <inheritdoc/>
        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _repository.GetByIdAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(User user)
        {
            // Validación de disponibilidad excluyendo el usuario actual
            if (!await IsUsernameOrEmailAvailableAsync(user.Username, user.Email, user.UserId))
                throw new TaskCanceledException("El nombre de usuario o correo electrónico no está disponible.");

            // Obtención del usuario existente
            User? _user = await GetByIdAsync(user.UserId)
                ?? throw new TaskCanceledException("El usuario no existe.");

            // Actualización de campos permitidos
            _user.RoleId = user.RoleId;
            _user.Username = user.Username;
            _user.Email = user.Email;

            return await _repository.UpdateAsync(_user);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int userId)
        {
            return await _repository.DeleteAsync(userId);
        }
        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// Utiliza <see cref="PasswordUtility.VerifyPassword"/> con protección contra ataques de tiempo.
        /// </remarks>
        public async Task<User> SignInAsync(string username, string password)
        {
            // Busqueda del usuario activo
            User? user = await _repository.GetByFilterAsync(u => (u.Username == username || u.Email == username) && u.IsActive)
                ?? throw new TaskCanceledException("No se encontró un usuario que coincida con la información proporcionada.");

            // Verificación de contraseña
            if (PasswordUtility.VerifyPassword(user.Salt, user.Password, password))
                return user;
            else
                throw new TaskCanceledException("La contraseña es incorrecta.");
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para><strong>Flujo de ejecución:</strong></para>
        /// <ol>
        /// <li>Genera nuevo salt y hash usando <see cref="PasswordUtility"/></li>
        /// <li>Invalida códigos de recuperación previos</li>
        /// <li>Envía confirmación por correo usando <see cref="IEmailService"/></li>
        /// </ol>
        /// </remarks>
        public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
        {
            User? user = await GetByIdAsync(userId)
                ?? throw new TaskCanceledException("El usuario no existe.");

            byte[] salt = PasswordUtility.GenerateSalt();
            byte[] encryptedPassword = PasswordUtility.EncryptPassword(salt, newPassword);

            user.Salt = salt;
            user.Password = encryptedPassword;
            user.RecoveryCode = null;
            user.ExpirationCode = null;
            user.IsPasswordReset = false;
            user.IsPasswordDefect = false;
            user.LastPasswordReset = DateTime.Now;

            if (await _repository.UpdateAsync(user))
            {
                string htmlContent = GetMessageText("BLL.Resources.PasswordChange.html");
                string messageBody = htmlContent.Replace("{Username}", user.Username);
                return await _emailService.SendEmailAsync(user.Email, "Confirmación de cambio de contraseña", messageBody);
            }

            return false;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para><strong>Detalles técnicos:</strong></para>
        /// <ul>
        /// <li>Genera código usando <see cref="Guid.NewGuid"/> truncado a 16 caracteres</li>
        /// <li>Establece expiración en <see cref="VALID_TIME"/> hora(s)</li>
        /// </ul>
        /// </remarks>
        public async Task<bool> SetRecoveryCodeAsync(string username)
        {
            User user = await _repository.GetByFilterAsync(u => u.Username == username || u.Email == username)
                ?? throw new TaskCanceledException("No se encontró un usuario que coincida con la información proporcionada.");

            string recoveryCode = Guid.NewGuid().ToString("N").Substring(0, 16);

            user.RecoveryCode = recoveryCode;
            user.ExpirationCode = DateTime.Now.AddHours(VALID_TIME);
            user.IsPasswordReset = true;
            user.LastPasswordReset = DateTime.Now;

            if (await _repository.UpdateAsync(user))
            {
                string validTime = VALID_TIME == 1 ? "1 hora" : $"{VALID_TIME} horas";
                string htmlContent = GetMessageText("BLL.Resources.ResetPassword.html");
                string messageBody = htmlContent.Replace("{Username}", user.Username).Replace("{RecoveryCode}", recoveryCode).Replace("{Valid-Time}", validTime);

                return await _emailService.SendEmailAsync(user.Email, "Restablecimiento de contraseña", messageBody);
            }

            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsernameOrEmailAvailableAsync(string username, string email, int? userId = null)
        {
            // Consulta que excluye el usuario actual en actualizaciones
            User? user = await _repository.GetByFilterAsync(u => (u.Username == username || u.Email == (email ?? username)) && (!userId.HasValue || u.UserId != userId.Value));

            return user == null;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para><strong>Lógica de validación:</strong></para>
        /// <ul>
        /// <li>Compara código en base de datos con <see cref="StringComparison.Ordinal"/></li>
        /// <li>Verifica <see cref="DateTime.Now"/> contra fecha de expiración</li>
        /// </ul>
        /// </remarks>
        public async Task<bool> IsValidRecoveryCodeAsync(string recoveryCode, string newPassword)
        {
            User? user = await _repository.GetByFilterAsync(u => u.RecoveryCode == recoveryCode && u.ExpirationCode > DateTime.Now)
                ?? throw new TaskCanceledException("El código de recuperación es inválido o ha expirado.");

            return await ChangePasswordAsync(user.UserId, newPassword);
        }

        private static string GetMessageText(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream? file = assembly.GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException("No se encontró el archivo de recursos.", resourceName);

            using (Stream stream = file)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
