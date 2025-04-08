using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación concreta del servicio para la gestión de materias académicas.
    /// </summary>
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject> _repository;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de materias.
        /// </summary>
        /// <param name="repository">Repositorio genérico para operaciones CRUD de materias.</param>
        public SubjectService(IGenericRepository<Subject> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<Subject> CreateAsync(Subject subject)
        {
            Subject? oSubject = await _repository.GetByFilterAsync(s => s.Code == subject.Code);

            if (oSubject != null)
                throw new TaskCanceledException("El código no se encuentra disponible.");

            Subject _subject = await _repository.AddAsync(subject);

            if (_subject.SubjectId == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar registrar la materia.");

            return _subject;
        }

        /// <inheritdoc/>
        public async Task<Subject> GetByIdAsync(int subjectId)
        {
            return await _repository.GetByIdAsync(subjectId)
                ?? throw new TaskCanceledException("No se ha encontrado una materia con la información proporcionada.");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(Subject subject)
        {
            Subject? oSubject = await _repository.GetByFilterAsync(s => s.Code == subject.Code && s.SubjectId != subject.SubjectId);

            if (oSubject != null)
                throw new TaskCanceledException("El código no se encuentra disponible.");

            Subject _subject = await GetByIdAsync(subject.SubjectId);

            _subject.Code = subject.Code;
            _subject.Name = subject.Name;
            _subject.Description = subject.Description;

            return await _repository.UpdateAsync(_subject);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int subjectId)
        {
            Subject subject = await GetByIdAsync(subjectId);

            subject.IsActive = false;

            return await _repository.UpdateAsync(subject);
        }
    }
}
