using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define las operaciones para administrar la asignación de estudiantes a aulas.
    /// </summary>
    public interface IClassroomStudentService
    {
        /// <summary>
        /// Crea una nueva asignación de estudiante a aula.
        /// </summary>
        /// <param name="student">Objeto ClassroomStudent con los datos de la asignación.</param>
        /// <returns>El ClassroomStudent creado.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el estudiante ya está asignado al aula o ocurre un error al registrar.</exception>
        Task<ClassroomStudent> CreateAsync(ClassroomStudent student);

        /// <summary>
        /// Obtiene una asignación estudiante-aula por su ID.
        /// </summary>
        /// <param name="id">ID de la asignación.</param>
        /// <returns>El ClassroomStudent correspondiente al ID.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si no se encuentra la asignación.</exception>
        Task<ClassroomStudent> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las asignaciones de estudiantes en un aula específica.
        /// </summary>
        /// <param name="classroomId">ID del aula.</param>
        /// <returns>Una colección de ClassroomStudent asociados al aula.</returns>
        Task<IEnumerable<ClassroomStudent>> GetAllByClassroomIdAsync(int classroomId);

        /// <summary>
        /// Actualiza una asignación estudiante-aula existente.
        /// </summary>
        /// <param name="student">Objeto ClassroomStudent con los datos actualizados.</param>
        /// <returns><c>true</c> si la actualización fue exitosa; de lo contrario, <c>false</c>.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el estudiante ya está asignado al aula.</exception>
        Task<bool> UpdateAsync(ClassroomStudent student);

        /// <summary>
        /// Elimina una asignación estudiante-aula por su ID.
        /// </summary>
        /// <param name="id">ID de la asignación a eliminar.</param>
        /// <returns><c>true</c> si la eliminación fue exitosa; de lo contrario, <c>false</c>.</returns>
        Task<bool> DeleteAsync(int id);
    }
}