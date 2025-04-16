using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación de las operaciones para gestionar la relación entre aulas y materias.
    /// </summary>
    public class ClassroomSubjectService : IClassroomSubjectService
    {
        private readonly IGenericRepository<ClassroomSubject> _repository;

        /// <summary>
        /// Inicializa una nueva instancia del servicio con un repositorio específico.
        /// </summary>
        /// <param name="repository">Repositorio para operaciones de persistencia.</param>
        public ClassroomSubjectService(IGenericRepository<ClassroomSubject> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        /// <remarks>
        /// Valida que no exista una relación duplicada antes de crear una nueva.
        /// </remarks>
        /// <exception cref="TaskCanceledException">
        /// Se lanza si ya existe una relación para el aula y materia proporcionadas,
        /// o si ocurre un error durante la creación.
        /// </exception>
        public async Task<ClassroomSubject> CreateAsync(ClassroomSubject classroomSubject)
        {
            ClassroomSubject? oClassroomSubject = await _repository.GetByFilterAsync(c => c.ClassroomId == classroomSubject.ClassroomId && c.SubjectId == classroomSubject.SubjectId);

            if (oClassroomSubject != null)
                throw new TaskCanceledException("La materia ya se encuentra registrada al grupo.");

            ClassroomSubject _classroomSubject = await _repository.AddAsync(classroomSubject);

            if (_classroomSubject.Id == 0)
                throw new TaskCanceledException("Ocurrió un problema al intentar registrar la materia al grupo.");

            return _classroomSubject;
        }

        /// <inheritdoc />
        /// <exception cref="TaskCanceledException">
        /// Se lanza si no se encuentra una relación con el ID especificado.
        /// </exception>
        public async Task<ClassroomSubject> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id)
                ?? throw new TaskCanceledException("No se ha encontrado una relación con la información proporcionada.");
        }

        public async Task<IEnumerable<ClassroomSubject>> GetAllByClassroomIdAsync(int classroomId)
        {
            return await _repository.GetAllAsync(c => c.ClassroomId == classroomId);
        }

        /// <inheritdoc />
        /// <remarks>
        /// Valida que los cambios no generen una relación duplicada (excluyendo el registro actual).
        /// </remarks>
        /// <exception cref="TaskCanceledException">
        /// Se lanza si la actualización genera un conflicto de duplicidad.
        /// </exception>
        public async Task<bool> Update(ClassroomSubject classroomSubject)
        {
            ClassroomSubject? oClassroomSubject = await _repository.GetByFilterAsync(c => c.ClassroomId == classroomSubject.ClassroomId && c.SubjectId == classroomSubject.SubjectId && c.Id != classroomSubject.Id);

            if (oClassroomSubject != null)
                throw new TaskCanceledException("La materia ya se encuentra registrada al grupo.");

            ClassroomSubject _classroomSubject = await GetByIdAsync(classroomSubject.Id);

            _classroomSubject.SubjectId = classroomSubject.SubjectId;

            return await _repository.UpdateAsync(_classroomSubject);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
