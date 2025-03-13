using DAL.DBContext;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Implementation
{
    /// <summary>
    /// Implementación concreta de <see cref="IGenericRepository{TEntity}"/> utilizando Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad, debe ser una clase.</typeparam>
    /// <remarks>
    /// <para>
    /// Esta implementación utiliza el patrón Unit of Work y está vinculada a un contexto de base de datos.
    /// </para>
    /// <para>
    /// Excepciones específicas de base de datos (ej: <see cref="DbUpdateException"/>) deben manejarse 
    /// en capas superiores o middleware.
    /// </para>
    /// </remarks>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AssistivaContext _dbContext;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos de Entity Framework.</param>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si <paramref name="dbContext"/> es <c>null</c>.
        /// </exception>
        public GenericRepository(AssistivaContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// Este método utiliza <see cref="DbContext.AddAsync"/> para operaciones asíncronas 
        /// y guarda cambios inmediatamente.
        /// </para>
        /// <para>
        /// Para mejorar el rendimiento en inserciones masivas, considere usar 
        /// <see cref="DbContext.AddRangeAsync"/> y guardar cambios una sola vez.
        /// </para>
        /// </remarks>
        /// <exception cref="DbUpdateException">
        /// Error durante la inserción en la base de datos (ej: violación de restricciones).
        /// </exception>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Este método utiliza <see cref="DbSet{TEntity}.FindAsync"/> que busca primero en la memoria 
        /// antes de consultar la base de datos.
        /// </remarks>
        /// <exception cref="SqlException">
        /// Error de conexión o consulta a la base de datos.
        /// </exception>
        public async Task<TEntity?> GetByIdAsync(int entityId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(entityId);

            try
            {
                return await _dbContext.Set<TEntity>().FindAsync(entityId);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Este método utiliza <see cref="DbSet{TEntity}.Where"/> para aplicar el filtro 
        /// y <see cref="DbSet{TEntity}.FirstOrDefaultAsync"/> para retornar el primer resultado.
        /// </remarks>
        /// <exception cref="SqlException">
        /// Error de conexión o consulta a la base de datos.
        /// </exception>
        public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentNullException.ThrowIfNull(filter, nameof(filter));

            try
            {
                return await _dbContext.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Este método utiliza <see cref="DbSet{TEntity}.ToListAsync"/> para retornar 
        /// todas las entidades de la base de datos.
        /// </remarks>
        /// <exception cref="SqlException">
        /// Error de conexión o consulta a la base de datos.
        /// </exception>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Este método utiliza <see cref="DbSet{TEntity}.Where"/> para aplicar el filtro 
        /// y <see cref="DbSet{TEntity}.ToListAsync"/> para retornar los resultados.
        /// </remarks>
        /// <exception cref="SqlException">
        /// Error de conexión o consulta a la base de datos.
        /// </exception>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentNullException.ThrowIfNull(filter, nameof(filter));

            try
            {
                return await _dbContext.Set<TEntity>().Where(filter).ToListAsync();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Este método marca la entidad como modificada usando <see cref="DbContext.Entry"/> 
        /// y actualiza todas sus propiedades.
        /// </remarks>
        /// <exception cref="DbUpdateConcurrencyException">
        /// Se lanza si la entidad fue modificada o eliminada por otro proceso después de ser cargada.
        /// </exception>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Este método busca la entidad por su ID y la elimina si existe.
        /// </remarks>
        /// <exception cref="DbUpdateException">
        /// Error durante la eliminación en la base de datos.
        /// </exception>
        public async Task<bool> DeleteAsync(int entityId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(entityId, nameof(entityId));

            try
            {
                TEntity? entity = await GetByIdAsync(entityId);

                if (entity == null) return false;

                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
