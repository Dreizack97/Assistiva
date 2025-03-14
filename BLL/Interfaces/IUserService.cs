using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define operaciones para la gestión de usuarios, incluyendo CRUD y autenticación.
    /// </summary>
    public interface IUserService
    {
        #region CRUD
        /// <summary>
        /// Crea un nuevo usuario de manera asíncrona.
        /// </summary>
        /// <param name="user">Instancia del usuario a crear.</param>
        /// <returns>Usuario creado con propiedades actualizadas (ej: ID asignado).</returns>
        /// <exception cref="TaskCanceledException">
        /// Se lanza si el nombre de usuario o correo electrónico ya están en uso.
        /// </exception>
        Task<User> CreateAsync(User user);

        /// <summary>
        /// Obtiene un usuario por su ID de manera asíncrona.
        /// </summary>
        /// <param name="userId">ID único del usuario.</param>
        /// <returns>Usuario encontrado o <c>null</c> si no existe.</returns>
        Task<User?> GetByIdAsync(int userId);

        /// <summary>
        /// Obtiene todos los usuarios de manera asíncrona.
        /// </summary>
        /// <returns>Colección de usuarios registrados.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Actualiza un usuario existente de manera asíncrona.
        /// </summary>
        /// <param name="user">Instancia del usuario con datos actualizados.</param>
        /// <returns><c>true</c> si la actualización fue exitosa; de lo contrario, <c>false</c>.</returns>
        /// <exception cref="TaskCanceledException">
        /// Se lanza si el nombre de usuario o correo electrónico ya están en uso, o si el usuario no existe.
        /// </exception>
        Task<bool> UpdateAsync(User user);

        /// <summary>
        /// Elimina un usuario por su ID de manera asíncrona.
        /// </summary>
        /// <param name="userId">ID único del usuario a eliminar.</param>
        /// <returns><c>true</c> si la eliminación fue exitosa; de lo contrario, <c>false</c>.</returns>
        Task<bool> DeleteAsync(int userId);
        #endregion

        /// <summary>
        /// Autentica a un usuario mediante nombre de usuario y contraseña.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Usuario autenticado.</returns>
        /// <exception cref="TaskCanceledException">
        /// Se lanza si las credenciales son incorrectas o el usuario está inactivo.
        /// </exception>
        Task<User> SignInAsync(string username, string password);

        /// <summary>
        /// Cambia la contraseña de un usuario de manera asíncrona.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="newPassword">Nueva contraseña en texto plano.</param>
        /// <returns><c>True</c> si el cambio fue exitoso; de lo contrario, <c>False</c>.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el usuario no existe.</exception>
        Task<bool> ChangePasswordAsync(int userId, string newPassword);

        /// <summary>
        /// Genera y envía un código de recuperación para restablecer la contraseña.
        /// </summary>
        /// <param name="username">Nombre de usuario o correo electrónico.</param>
        /// <returns><c>True</c> si el código fue generado y enviado correctamente; de lo contrario, <c>False</c>.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si no se encuentra el usuario.</exception>
        Task<bool> SetRecoveryCodeAsync(string username);

        /// <summary>
        /// Verifica si un nombre de usuario o correo electrónico están disponibles.
        /// </summary>
        /// <param name="username">Nombre de usuario a verificar.</param>
        /// <param name="email">Correo electrónico a verificar.</param>
        /// <param name="userId">ID opcional del usuario (para excluirlo en actualizaciones).</param>
        /// <returns><c>true</c> si el nombre y correo están disponibles; de lo contrario, <c>false</c>.</returns>
        Task<bool> IsUsernameOrEmailAvailableAsync(string username, string email, int? userId = null);

        /// <summary>
        /// Valida un código de recuperación y cambia la contraseña si es válido.
        /// </summary>
        /// <param name="recoveryCode">Código de recuperación proporcionado por el usuario.</param>
        /// <param name="newPassword">Nueva contraseña en texto plano.</param>
        /// <returns><c>True</c> si la contraseña fue cambiada exitosamente; de lo contrario, <c>False</c>.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el código es inválido o ha expirado.</exception>
        Task<bool> IsValidRecoveryCodeAsync(string recoveryCode, string newPassword);
    }
}
