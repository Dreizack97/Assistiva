using System.Linq.Expressions;
using BLL.Implementation;
using DAL.Interfaces;
using Entity;
using Moq;

namespace TestProject
{
    public class ClassroomServiceTest
    {
        private Mock<IGenericRepository<Classroom>> _mockRepository;
        private ClassroomService _classroomService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericRepository<Classroom>>();
            _classroomService = new ClassroomService(_mockRepository.Object);
        }

        [Test]
        public async Task CreateAsync_ValidClassroom_ReturnsCreatedClassroom()
        {
            // Arrange
            Classroom newClassroom = new Classroom { Name = "Math 101", TeacherId = 1 };
            Classroom createdClassroom = new Classroom { ClassroomId = 1, Name = "Math 101", TeacherId = 1 };

            _mockRepository.Setup(repo => repo.AddAsync(newClassroom)).ReturnsAsync(createdClassroom);

            // Act
            Classroom result = await _classroomService.CreateAsync(newClassroom);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ClassroomId, Is.EqualTo(createdClassroom.ClassroomId));
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Classroom>()), Times.Once);
        }

        [Test]
        public async Task CreateAsync_ClassroomAlreadyExists_ThrowsException()
        {
            // Arrange
            Classroom newClassroom = new Classroom { Name = "Math 101", TeacherId = 1 };
            _mockRepository.Setup(repo => repo.GetByFilterAsync(c => c.Name == newClassroom.Name)).ReturnsAsync(newClassroom);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(async () => await _classroomService.CreateAsync(newClassroom));
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Classroom>()), Times.Never);
        }

        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsClassroom()
        {
            // Arrange
            int classroomId = 1;
            Classroom classroom = new Classroom { ClassroomId = classroomId, Name = "Math 101", TeacherId = 1 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(classroomId)).ReturnsAsync(classroom);

            // Act
            Classroom result = await _classroomService.GetByIdAsync(classroomId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ClassroomId, Is.EqualTo(classroomId));
            _mockRepository.Verify(repo => repo.GetByIdAsync(classroomId), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllClassrooms()
        {
            // Arrange
            var classrooms = new List<Classroom>
            {
                new Classroom { ClassroomId = 1, Name = "Math 101", TeacherId = 1 },
                new Classroom { ClassroomId = 2, Name = "Science 101", TeacherId = 2 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Classroom, object>>[]>())).ReturnsAsync(classrooms);

            // Act
            IEnumerable<Classroom> result = await _classroomService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<Classroom>>());
            Assert.That(result.Count(), Is.EqualTo(classrooms.Count()));
        }

        [Test]
        public async Task UpdateAsync_ValidClassroom_ReturnsTrue()
        {
            // Arrange
            Classroom classroomToUpdate = new Classroom { ClassroomId = 1, Name = "Math 101", TeacherId = 1 };
            Classroom updatedClassroom = new Classroom { ClassroomId = 1, Name = "Math 101", TeacherId = 2 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(classroomToUpdate.ClassroomId)).ReturnsAsync(classroomToUpdate);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Classroom>())).ReturnsAsync(true);

            // Act
            bool result = await _classroomService.UpdateAsync(updatedClassroom);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Classroom>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ClassroomNotFound_ReturnsFalse()
        {
            // Arrange
            Classroom classroomToUpdate = new Classroom { ClassroomId = 1, Name = "Math 101", TeacherId = 1 };
            Classroom classroomToFound = new Classroom { ClassroomId = 2, Name = "Math 102", TeacherId = 2 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(classroomToUpdate.ClassroomId)).ReturnsAsync(classroomToFound);

            // Act
            bool result = await _classroomService.UpdateAsync(classroomToUpdate);

            // Assert
            Assert.That(result, Is.False);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Classroom>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            int classroomId = 1;
            _mockRepository.Setup(repo => repo.DeleteAsync(classroomId)).ReturnsAsync(true);

            // Act
            bool result = await _classroomService.DeleteAsync(classroomId);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.DeleteAsync(classroomId), Times.Once);
        }
    }
}