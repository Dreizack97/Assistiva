using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define las operaciones del servicio para la gestión de materias académicas.
    /// </summary>
    public interface ISubjectService
    {
        /// <summary>
        /// Crea una nueva materia en el sistema.
        /// </summary>
        /// <param name="subject">Objeto Subject con los datos de la materia a crear.</param>
        /// <returns>Subject creada con su identificador asignado.</returns>
        /// <exception cref="TaskCanceledException">Se lanza cuando el código de materia ya existe o ocurre un error en la creación.</exception>
        Task<Subject> CreateAsync(Subject subject);

        /// <summary>
        /// Obtiene una materia específica por su identificador.
        /// </summary>
        /// <param name="subjectId">Identificador único de la materia.</param>
        /// <returns>Subject solicitada.</returns>
        /// <exception cref="TaskCanceledException">Se lanza cuando no se encuentra la materia.</exception>
        Task<Subject> GetByIdAsync(int subjectId);

        /// <summary>
        /// Obtiene todas las materias registradas en el sistema.
        /// </summary>
        /// <returns>Colección enumerable de Subjects.</returns>
        Task<IEnumerable<Subject>> GetAllAsync();

        /// <summary>
        /// Actualiza los datos de una materia existente.
        /// </summary>
        /// <param name="subject">Objeto Subject con los datos actualizados.</param>
        /// <returns><c>true</c> si la actualización fue exitosa, <c>false</c> en caso contrario.</returns>
        /// <exception cref="TaskCanceledException">Se lanza cuando el código de materia ya existe en otra materia.</exception>
        Task<bool> UpdateAsync(Subject subject);

        /// <summary>
        /// Realiza un borrado lógico de una materia (desactiva).
        /// </summary>
        /// <param name="subjectId">Identificador único de la materia a desactivar.</param>
        /// <returns><c>true</c> si la operación fue exitosa, <c>false</c> en caso contrario.</returns>
        /// <exception cref="TaskCanceledException">Se lanza cuando no se encuentra la materia.</exception>
        Task<bool> DeleteAsync(int subjectId);
    }
}
