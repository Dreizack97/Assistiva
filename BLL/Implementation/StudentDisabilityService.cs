using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <inheritdoc cref="IStudentDisabilityService"/>
    public class StudentDisabilityService : IStudentDisabilityService
    {
        private readonly IGenericRepository<StudentDisability> _repository;

        /// <summary>
        /// Inicializa una nueva instancia del servicio con el repositorio especificado.
        /// </summary>
        /// <param name="repository">Repositorio para acceder a los datos de StudentDisability.</param>
        public StudentDisabilityService(IGenericRepository<StudentDisability> repository)
        {
            _repository = repository;
        }
        
        /// <inheritdoc />
        /// <exception cref="TaskCanceledException">
        /// Se lanza cuando:
        /// <list type="bullet">
        /// <item>Ya existe una asociación idéntica para el estudiante</item>
        /// <item>Ocurre un error durante la creación del registro</item>
        /// </list>
        /// </exception>
        public async Task<StudentDisability> CreateAsync(StudentDisability studentDisability)
        {
            StudentDisability? oStudentDisability = await _repository.GetByFilterAsync(d => d.StudentId == studentDisability.StudentId && d.DisabilityId == studentDisability.DisabilityId);

            if (oStudentDisability != null)
                throw new TaskCanceledException("El estudiante ya cuenta una esta discapacidad registrada.");

            StudentDisability _studentDisability = await _repository.AddAsync(studentDisability);

            if (_studentDisability.Id == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar registrar la discapacidad.");

            return _studentDisability;
        }

        /// <inheritdoc />
        /// <exception cref="TaskCanceledException">Se lanza cuando no se encuentra el registro con el ID especificado.</exception>
        public async Task<StudentDisability> GetByIdAsync(int id)
        {
            StudentDisability? studentDisability = await _repository.GetByIdAsync(id)
                ?? throw new TaskCanceledException("No se encontró registro con la información proporcionada.");

            return studentDisability;
        }

        /// <inheritdoc />
        /// <returns>
        /// Una colección de <see cref="StudentDisability"/> que puede estar vacía si no se encuentran resultados.
        /// </returns>
        public async Task<IEnumerable<StudentDisability>> GetAllByStudentIdAsync(int studentId)
        {
            return await _repository.GetAllAsync(d => d.StudentId == studentId);
        }

        // <inheritdoc />
        /// <exception cref="TaskCanceledException">
        /// Se lanza cuando:
        /// <list type="bullet">
        /// <item>No se encuentra el registro a actualizar</item>
        /// <item>Ya existe otra asociación idéntica para el estudiante</item>
        /// </list>
        /// </exception>
        public async Task<bool> UpdateAsync(StudentDisability studentDisability)
        {
            StudentDisability? oStudentDisability = await _repository.GetByFilterAsync(d => d.StudentId == studentDisability.StudentId && d.DisabilityId == studentDisability.DisabilityId 
                && d.Id != studentDisability.Id);

            if (oStudentDisability != null)
                throw new TaskCanceledException("El estudiante ya cuenta una esta discapacidad registrada.");

            StudentDisability _studentDisability = await GetByIdAsync(studentDisability.Id);

            _studentDisability.DisabilityId = studentDisability.DisabilityId;

            return await _repository.UpdateAsync(_studentDisability);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}