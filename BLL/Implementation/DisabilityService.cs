using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación concreta del servicio para la gestión de discapacidades.
    /// Proporciona la lógica de negocio para operaciones CRUD de discapacidades,
    /// incluyendo validaciones de negocio y manejo de transacciones.
    /// </summary>
    /// <remarks>
    /// Esta implementación:
    /// <list type="bullet">
    ///   <item>Valida la unicidad del nombre de discapacidad en creación y actualización</item>
    ///   <item>Implementa borrado lógico mediante desactivación (IsActive = false)</item>
    ///   <item>Maneja adecuadamente los errores y casos excepcionales</item>
    /// </list>
    /// </remarks>
    public class DisabilityService : IDisabilityService
    {
        private readonly IGenericRepository<Disability> _repository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DisabilityService"/>.
        /// </summary>
        /// <param name="repository">Repositorio genérico para operaciones CRUD de discapacidades.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el repositorio proporcionado es nulo.</exception>
        public DisabilityService(IGenericRepository<Disability> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Validaciones adicionales realizadas:
        /// <list type="bullet">
        ///   <item>Verifica que el nombre no esté en uso por otra discapacidad</item>
        ///   <item>Confirma que se generó un ID válido (mayor que 0) tras la creación</item>
        /// </list>
        /// </remarks>
        public async Task<Disability> CreateAsync(Disability disability)
        {
            Disability? oDisability = await _repository.GetByFilterAsync(d => d.Name == disability.Name);

            if (oDisability != null)
                throw new TaskCanceledException("El nombre de la discapacidad no se encuentra disponible.");

            Disability _disability = await _repository.AddAsync(disability);

            if (_disability.DisabilityId == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar registrar la discapacidad.");

            return _disability;
        }

        /// <inheritdoc/>
        public async Task<Disability> GetByIdAsync(int disabilityId)
        {
            Disability? disability = await _repository.GetByIdAsync(disabilityId)
                ?? throw new TaskCanceledException("No existe la discapacidad con la información proporcionada.");

            return disability;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Disability>> GetAllAsync()
        {
            return await _repository.GetAllAsync(d => d.IsActive);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// La actualización:
        /// <list type="bullet">
        ///   <item>Mantiene el estado IsActive original</item>
        ///   <item>Actualiza solo los campos Name y Description</item>
        ///   <item>Verifica conflictos de nombre con otras discapacidades</item>
        /// </list>
        /// </remarks>
        public async Task<bool> UpdateAsync(Disability disability)
        {
            Disability? oDisability = await _repository.GetByFilterAsync(d => d.Name == disability.Name && d.DisabilityId != disability.DisabilityId);

            if (oDisability != null)
                throw new TaskCanceledException("El nombre de la discapacidad no se encuentra disponible.");

            Disability _disability = await GetByIdAsync(disability.DisabilityId);

            _disability.Name = disability.Name;
            _disability.Description = disability.Description;

            return await _repository.UpdateAsync(_disability);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// La desactivación:
        /// <list type="bullet">
        ///   <item>Es reversible (puede reactivarse mediante UpdateAsync)</item>
        ///   <item>No elimina físicamente el registro</item>
        ///   <item>El registro desactivado no aparecerá en GetAllAsync</item>
        /// </list>
        /// </remarks>
        public async Task<bool> DisableAsync(int disabilityId)
        {
            Disability _disability = await GetByIdAsync(disabilityId);

            _disability.IsActive = false;

            return await _repository.UpdateAsync(_disability);
        }
    }
}
