using BLL.Interfaces;
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

        /// <summary>
        /// Inicializa una nueva instancia del servicio de usuarios.
        /// </summary>
        /// <param name="repository">Repositorio genérico para operaciones CRUD.</param>
        public UserService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        #region CRUD
        /// <inheritdoc/>
        public async Task<User> CreateAsync(User user)
        {
            // Validación de disponibilidad de nombre de usuario y correo electrónico
            if (!await IsUsernameOrEmailAvailable(user.Username, user.Email))
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
            if (!await IsUsernameOrEmailAvailable(user.Username, user.Email, user.UserId))
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
        public async Task<bool> IsUsernameOrEmailAvailable(string username, string email, int? userId = null)
        {
            // Consulta que excluye el usuario actual en actualizaciones
            User? user = await _repository.GetByFilterAsync(u => (u.Username == username || u.Email == (email ?? username)) && (!userId.HasValue || u.UserId != userId.Value));

            return user == null;
        }
    }
}
