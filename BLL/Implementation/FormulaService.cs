using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación del servicio para la gestión de fórmulas matemáticas.
    /// </summary>
    public class FormulaService : IFormulaService
    {
        private readonly IGenericRepository<Formula> _repository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FormulaService"/>.
        /// </summary>
        /// <param name="repository">Repositorio genérico para operaciones CRUD de fórmulas.</param>
        public FormulaService(IGenericRepository<Formula> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<Formula> CreateAsync(Formula formula)
        {
            Formula? oFormula = await _repository.GetByFilterAsync(f => f.Name == formula.Name && f.SubjectId == formula.SubjectId);

            if (oFormula != null)
                throw new TaskCanceledException("Ya existe una fórmula con el mismo nombre para la misma asignatura.");

            Formula _formula = await _repository.AddAsync(formula);

            if (_formula.FormulaId == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar registrar la fórmula.");

            return _formula;
        }

        /// <inheritdoc/>
        public async Task<Formula> GetByIdAsync(int formulaId)
        {
            return await _repository.GetByIdAsync(formulaId)
                ?? throw new TaskCanceledException("No se ha encontrado una fórmula con la información proporcionada.");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Formula>> GetAllBySubjectIdAsync(int subjectId)
        {
            return await _repository.GetAllAsync(f => f.SubjectId == subjectId);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(Formula formula)
        {
            Formula? oFormula = await _repository.GetByFilterAsync(f => f.Name == formula.Name && f.SubjectId == formula.SubjectId && f.FormulaId != formula.FormulaId);

            if (oFormula != null)
                throw new TaskCanceledException("Ya existe una fórmula con el mismo nombre para la misma asignatura.");

            Formula _formula = await GetByIdAsync(formula.FormulaId);

            _formula.Name = formula.Name;
            _formula.Content = formula.Content;
            _formula.Description = formula.Description;

            return await _repository.UpdateAsync(_formula);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int formulaId)
        {
            return await _repository.DeleteAsync(formulaId);
        }
    }
}
