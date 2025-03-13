using System.Linq.Expressions;

namespace DAL.Interfaces
{
    /// <summary>
    /// Define un contrato genérico para operaciones CRUD (Crear, Leer, Actualizar, Eliminar) asíncronas 
    /// sobre entidades de base de datos.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad, debe ser una clase.</typeparam>
    /// <remarks>
    /// Esta interfaz abstrae el acceso a datos, permitiendo intercambiar implementaciones 
    /// (Entity Framework, Dapper, etc.) sin afectar a las capas superiores.
    /// </remarks>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Agrega una nueva entidad de tipo <typeparamref name="TEntity"/> de manera asíncrona.
        /// </summary>
        /// <param name="entity">Instancia de la entidad a agregar.</param>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Contiene la entidad añadida 
        /// con propiedades actualizadas (ej: ID generado).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si <paramref name="entity"/> es <c>null</c>.
        /// </exception>
        /// <example>
        /// <code>
        /// var producto = new Producto { Nombre = "Laptop" };
        /// var resultado = await _repositorio.AddAsync(producto);
        /// </code>
        /// </example>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Obtiene una entidad de tipo <typeparamref name="TEntity"/> por su ID de manera asíncrona.
        /// </summary>
        /// <param name="entityId">ID único de la entidad (clave primaria).</param>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Contiene la entidad encontrada 
        /// o <c>null</c> si no existe.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se lanza si <paramref name="entityId"/> es menor o igual a cero.
        /// </exception>
        Task<TEntity?> GetByIdAsync(int entityId);

        /// <summary>
        /// Obtiene una entidad de tipo <typeparamref name="TEntity"/> que cumple con el filtro especificado.
        /// </summary>
        /// <param name="filter">Expresión lambda que define el criterio de búsqueda.</param>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Contiene la entidad que cumple 
        /// con el filtro o <c>null</c> si no se encuentra.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si <paramref name="filter"/> es <c>null</c>.
        /// </exception>
        Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Obtiene todas las entidades de tipo <typeparamref name="TEntity"/> de manera asíncrona.
        /// </summary>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Contiene una colección de todas 
        /// las entidades disponibles.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Obtiene todas las entidades de tipo <typeparamref name="TEntity"/> que cumplen con el filtro especificado.
        /// </summary>
        /// <param name="filter">Expresión lambda que define el criterio de filtrado.</param>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Contiene una colección de entidades 
        /// que cumplen con el filtro.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si <paramref name="filter"/> es <c>null</c>.
        /// </exception>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Actualiza una entidad existente de tipo <typeparamref name="TEntity"/> de manera asíncrona.
        /// </summary>
        /// <param name="entity">Instancia de la entidad con los datos actualizados.</param>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Retorna <c>true</c> si la actualización 
        /// fue exitosa; de lo contrario, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si <paramref name="entity"/> es <c>null</c>.
        /// </exception>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Elimina una entidad de tipo <typeparamref name="TEntity"/> por su ID de manera asíncrona.
        /// </summary>
        /// <param name="entityId">ID único de la entidad a eliminar.</param>
        /// <returns>
        /// Tarea que representa la operación asíncrona. Retorna <c>true</c> si la eliminación 
        /// fue exitosa; de lo contrario, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se lanza si <paramref name="entityId"/> es menor o igual a cero.
        /// </exception>
        Task<bool> DeleteAsync(int entityId);
    }
}
