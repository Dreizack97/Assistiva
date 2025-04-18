using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define operaciones para gestionar la relación entre aulas (classrooms) y materias (subjects).
    /// </summary>
    public interface IClassroomSubjectService
    {
        /// <summary>
        /// Crea una nueva relación entre un aula y una materia.
        /// </summary>
        /// <param name="classroomSubject">Objeto con los datos de la relación a crear.</param>
        /// <returns>La relación creada con su ID generado.</returns>
        Task<ClassroomSubject> CreateAsync(ClassroomSubject classroomSubject);

        /// <summary>
        /// Obtiene una relación específica por su ID.
        /// </summary>
        /// <param name="id">ID de la relación a buscar.</param>
        /// <returns>La relación encontrada.</returns>
        Task<ClassroomSubject> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las relaciones asociadas a un aula específica.
        /// </summary>
        /// <param name="classroomId">ID del aula para filtrar las relaciones.</param>
        /// <returns>Colección de relaciones encontradas.</returns>
        Task<IEnumerable<ClassroomSubject>> GetAllByClassroomIdAsync(int classroomId);


        /// <summary>
        /// Obtiene todas las relaciones asociadas a un estudiante específico.
        /// </summary>
        /// <param name="studentId">Id del estudiante para filtrar relaciones.</param>
        /// <returns>Colección de relaciones encontradas.</returns>
        Task<IEnumerable<ClassroomSubject>> GetAllByStudentIdAsync(int studentId);

        /// <summary>
        /// Actualiza los datos de una relación existente.
        /// </summary>
        /// <param name="classroomSubject">Objeto con los nuevos datos de la relación.</param>
        /// <returns><c>true</c> si la actualización fue exitosa.</returns>
        Task<bool> UpdateAsync(ClassroomSubject classroomSubject);

        /// <summary>
        /// Elimina una relación por su ID.
        /// </summary>
        /// <param name="id">ID de la relación a eliminar.</param>
        /// <returns><c>true</c> si la eliminación fue exitosa.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
