using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación del servicio de gestión de estudiantes.
    /// </summary>
    /// <remarks>
    /// Esta clase maneja la lógica de negocio relacionada con los estudiantes,
    /// incluyendo su creación, actualización y desactivación, así como la gestión
    /// de usuarios asociados. Utiliza un repositorio genérico para operaciones CRUD
    /// y un servicio de usuarios para gestionar las cuentas relacionadas.
    /// </remarks>
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _repository;
        private readonly IUserService _userService;
        private const int ROLE_ID_STUDENT = 5;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="StudentService"/>.
        /// </summary>
        /// <param name="repository">Repositorio para operaciones con estudiantes.</param>
        /// <param name="userService">Servicio para gestión de usuarios asociados.</param>
        public StudentService(IGenericRepository<Student> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Además de crear el estudiante, genera automáticamente un nombre de usuario
        /// combinando la primera letra del nombre, el apellido paterno y el ID del estudiante
        /// con ceros a la izquierda. Crea también un usuario asociado con rol de estudiante.
        /// </remarks>
        public async Task<Student> CreateAsync(Student student, string email)
        {
            IEnumerable<Student> students = await _repository.GetAllAsync();
            int consecutive = students.Count() + 1;
            string username = string.Join("", [student.FirstName[0], student.PaternalLastName, consecutive.ToString().PadLeft(6, '0')]);

            User user = new User()
            {
                RoleId = ROLE_ID_STUDENT,
                Username = username,
                Email = email,
                UrlPicture = student.PhotoUrl
            };

            user = await _userService.CreateAsync(user);

            if (user.UserId > 0)
            {
                student.UserId = user.UserId;
                student.IsActive = true;

                Student _student = await _repository.AddAsync(student);

                if (_student.StudentId == 0)
                    throw new TaskCanceledException("Ocurrió un error al intentar registrar al alumno.");

                return _student;
            }

            throw new TaskCanceledException("Ocurrió un error al intentar registrar al alumno.");
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Si no se encuentra el estudiante, lanza una excepción TaskCanceledException.
        /// </remarks>
        public async Task<Student> GetByIdAsync(int studentId)
        {
            Student? student = await _repository.GetByFilterAsync(s => s.StudentId == studentId, [u => u.User])
                ?? throw new TaskCanceledException("No existe el estudiante con la información proporcionada.");

            return student;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _repository.GetAllAsync(s => s.IsActive);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Actualiza todos los campos modificables del estudiante excepto el ID y el estado IsActive.
        /// </remarks>
        public async Task<bool> UpdateAsync(Student student)
        {
            Student _student = await GetByIdAsync(student.StudentId);

            _student.FirstName = student.FirstName;
            _student.PaternalLastName = student.PaternalLastName;
            _student.MaternalLastName = student.MaternalLastName;
            _student.Gender = student.Gender;
            _student.DateOfBirth = student.DateOfBirth;
            _student.EducationLevel = student.EducationLevel;
            _student.Profession = student.Profession;
            _student.ProfessionStatus = student.ProfessionStatus;
            _student.MaritalStatus = student.MaritalStatus;
            _student.BloodType = student.BloodType;
            _student.Street = student.Street;
            _student.Number = student.Number;
            _student.Neighborhood = student.Neighborhood;
            _student.City = student.City;
            _student.PostalCode = student.PostalCode;
            _student.Country = student.Country;
            _student.PhotoUrl = student.PhotoUrl;

            return await _repository.UpdateAsync(_student);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Realiza una desactivación lógica (soft delete) cambiando el estado IsActive a false.
        /// </remarks>
        public async Task<bool> DisableAsync(int studentId)
        {
            Student student = await GetByIdAsync(studentId);

            student.IsActive = false;

            return await _repository.UpdateAsync(student);
        }
    }
}
