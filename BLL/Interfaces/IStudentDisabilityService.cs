using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define las operaciones del servicio para gestionar las discapacidades de estudiantes.
    /// </summary>
    public interface IStudentDisabilityService
    {
        /// <summary>
        /// Crea una nueva relación entre un estudiante y una discapacidad.
        /// </summary>
        /// <param name="studentDisability">Objeto StudentDisability con los datos a registrar.</param>
        /// <returns>El objeto StudentDisability creado con su ID generado.</returns>
        Task<StudentDisability> CreateAsync(StudentDisability studentDisability);

        /// <summary>
        /// Obtiene una relación estudiante-discapacidad por su ID.
        /// </summary>
        /// <param name="id">ID de la relación a buscar.</param>
        /// <returns>El objeto StudentDisability encontrado.</returns>
        Task<StudentDisability> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las discapacidades asociadas a un estudiante específico.
        /// </summary>
        /// <param name="studentId">ID del estudiante a consultar.</param>
        /// <returns>Colección de objetos StudentDisability asociados al estudiante.</returns>
        Task<IEnumerable<StudentDisability>> GetAllByStudentIdAsync(int studentId);

        /// <summary>
        /// Actualiza la información de una relación estudiante-discapacidad existente.
        /// </summary>
        /// <param name="studentDisability">Objeto StudentDisability con los datos actualizados.</param>
        /// <returns><c>true</c> si la actualización fue exitosa, <c>false</c> en caso contrario.</returns>
        Task<bool> UpdateAsync(StudentDisability studentDisability);

        /// <summary>
        /// Elimina una relación estudiante-discapacidad.
        /// </summary>
        /// <param name="id">ID de la relación a eliminar.</param>
        /// <returns><c>true</c> si la eliminación fue exitosa, <c>false</c> en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}