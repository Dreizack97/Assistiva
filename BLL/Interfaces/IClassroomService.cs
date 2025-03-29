using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Proporciona operaciones para la gestión de aulas en el sistema.
    /// </summary>
    public interface IClassroomService
    {
        /// <summary>
        /// Crea un nuevo aula validando la unicidad del nombre.
        /// </summary>
        /// <param name="classroom">Objeto Classroom con los datos del aula a registrar.</param>
        /// <returns>El aula creada con su ID generado.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el nombre ya existe o hay un error en el registro.</exception>
        Task<Classroom> CreateAsync(Classroom classroom);

        /// <summary>
        /// Obtiene un aula por su identificador único.
        /// </summary>
        /// <param name="classroomId">ID del aula a buscar.</param>
        /// <returns>El aula correspondiente al ID proporcionado.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si no se encuentra el aula.</exception>
        Task<Classroom> GetByIdAsync(int classroomId);

        /// <summary>
        /// Recupera todas las aulas registradas en el sistema.
        /// </summary>
        /// <returns>Una colección enumerable de aulas.</returns>
        Task<IEnumerable<Classroom>> GetAllAsync();

        /// <summary>
        /// Actualiza los datos de un aula existente.
        /// </summary>
        /// <param name="classroom">Objeto Classroom con los datos actualizados.</param>
        /// <returns><c>true</c> si la actualización fue exitosa, de lo contrario <c>false</c>.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el nuevo nombre ya está en uso.</exception>
        Task<bool> UpdateAsync(Classroom classroom);

        /// <summary>
        /// Elimina un aula del sistema mediante su ID.
        /// </summary>
        /// <param name="classroomId">ID del aula a eliminar.</param>
        /// <returns><c>true</c> si la eliminación fue exitosa, de lo contrario <c>false</c>.</returns>
        Task<bool> DeleteAsync(int classroomId);
    }
}