using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface que define las operaciones del servicio de gestión de estudiantes.
    /// Proporciona métodos para crear, obtener, actualizar y desactivar estudiantes,
    /// así como para gestionar su relación con usuarios del sistema.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Crea un nuevo estudiante y su usuario asociado en el sistema.
        /// </summary>
        /// <param name="student">Objeto Student con los datos del estudiante.</param>
        /// <param name="email">Correo electrónico para la cuenta de usuario asociada.</param>
        /// <returns>El estudiante creado con su ID generado.</returns>
        Task<Student> CreateAsync(Student student, string email);

        /// <summary>
        /// Obtiene un estudiante por su ID.
        /// </summary>
        /// <param name="studentId">ID del estudiante a buscar.</param>
        /// <returns>El estudiante encontrado.</returns>
        /// <exception cref="TaskCanceledException">Se lanza cuando no se encuentra el estudiante.</exception>
        Task<Student> GetByIdAsync(int studentId);

        /// <summary>
        /// Obtiene un estudiante por su nombre.
        /// </summary>
        /// <param name="studentName">Nombre del estudiante a buscar.</param>
        /// <returns>El estudiante encontrado.</returns>
        Task<Student> GetByNameAsync(string studentName);

        /// <summary>
        /// Obtiene todos los estudiantes registrados en el sistema.
        /// </summary>
        /// <returns>Una colección de todos los estudiantes.</returns>
        Task<IEnumerable<Student>> GetAllAsync();

        /// <summary>
        /// Actualiza la información de un estudiante existente.
        /// </summary>
        /// <param name="student">Objeto Student con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario.</returns>
        Task<bool> UpdateAsync(Student student);

        /// <summary>
        /// Desactiva un estudiante cambiando su estado IsActive a false.
        /// </summary>
        /// <param name="studentId">ID del estudiante a desactivar.</param>
        /// <returns>True si la operación fue exitosa, false en caso contrario.</returns>
        Task<bool> DisableAsync(int studentId);
    }
}
