using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define las operaciones del servicio para la gestión de discapacidades.
    /// Proporciona métodos para crear, obtener, actualizar y deshabilitar discapacidades.
    /// </summary>
    public interface IDisabilityService
    {
        /// <summary>
        /// Crea una nueva discapacidad de forma asíncrona.
        /// </summary>
        /// <param name="disability">Objeto Disability con los datos de la discapacidad a crear.</param>
        /// <returns>Task que representa la operación asíncrona. Retorna la discapacidad creada con su ID generado.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el nombre de la discapacidad ya existe o si ocurre un error durante la creación.</exception>
        Task<Disability> CreateAsync(Disability disability);

        /// <summary>
        /// Obtiene una discapacidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="disabilityId">ID de la discapacidad a buscar.</param>
        /// <returns>Task que representa la operación asíncrona. Retorna la discapacidad encontrada.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si no se encuentra la discapacidad con el ID proporcionado.</exception>
        Task<Disability> GetByIdAsync(int disabilityId);

        /// <summary>
        /// Obtiene todas las discapacidades de forma asíncrona.
        /// </summary>
        /// <returns>Task que representa la operación asíncrona. Retorna una colección de todas las discapacidades.</returns>
        Task<IEnumerable<Disability>> GetAllAsync();

        /// <summary>
        /// Actualiza una discapacidad existente de forma asíncrona.
        /// </summary>
        /// <param name="disability">Objeto Disability con los datos actualizados.</param>
        /// <returns>Task que representa la operación asíncrona. Retorna <c>true</c> si la actualización fue exitosa.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si el nombre de la discapacidad ya existe o si no se encuentra la discapacidad a actualizar.</exception>
        Task<bool> UpdateAsync(Disability disability);

        /// <summary>
        /// Deshabilita una discapacidad de forma asíncrona.
        /// </summary>
        /// <param name="disabilityId">ID de la discapacidad a deshabilitar.</param>
        /// <returns>Task que representa la operación asíncrona. Retorna <c>true</c> si la operación fue exitosa.</returns>
        /// <exception cref="TaskCanceledException">Se lanza si no se encuentra la discapacidad con el ID proporcionado.</exception>
        Task<bool> DisableAsync(int disabilityId);
    }
}
