using System.Linq.Expressions;
using AppUI.Controllers;
using BLL.Implementation;
using BLL.Interfaces;
using DAL.Interfaces;
using Entity;
using Moq;

namespace TestProject
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IGenericRepository<User>> _mockRepository;
        private Mock<IEmailService> _mockEmailService;
        private Mock<IUserService> _mockUserService;
        private SignInController _signInController;
        private UserService _userService;


        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericRepository<User>>();
            _mockEmailService = new Mock<IEmailService>();
            _userService = new UserService(_mockRepository.Object, _mockEmailService.Object);

            _mockUserService = new Mock<IUserService>();
            _signInController = new SignInController(_mockUserService.Object);
        }

        [Test]
        public async Task CreateAsync_ValidUser_ReturnsCreatedUser()
        {
            const string TEST_USERNAME = "TestUser";
            const string TEST_EMAIL = "test@example.com";
            const string EMAIL_SUBJECT = "Bienvenido a Assistiva";
            string EMAIL_BODY = It.IsAny<string>();

            // Arrange
            User newUser = new User { Username = TEST_USERNAME, Email = TEST_EMAIL };
            User createdUser = new User { UserId = 1, Username = TEST_USERNAME, Email = TEST_EMAIL };

            _mockRepository.Setup(repo => repo.GetByFilterAsync(u => u.Username == newUser.Username)).ReturnsAsync((User?)null);
            _mockRepository.Setup(repo => repo.AddAsync(newUser)).ReturnsAsync(createdUser);
            _mockEmailService.Setup(email => email.SendEmailAsync(TEST_EMAIL, EMAIL_SUBJECT, It.IsAny<string>(), null)).ReturnsAsync(true);

            // Act
            User result = await _userService.CreateAsync(newUser);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(createdUser.UserId));
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
            _mockEmailService.Verify(email => email.SendEmailAsync(TEST_EMAIL, EMAIL_SUBJECT, It.IsAny<string>(), null), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_ExistingUser_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User { UserId = userId, Username = "testuser", Email = "test@example.com" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            User? result = await _userService.GetByIdAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(expectedUser.UserId, Is.EqualTo(result.UserId));
            Assert.That(expectedUser.Username, Is.EqualTo(result.Username));
        }

        [Test]
        public async Task GetByIdAsync_NonExistingUser_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            // Act
            User? result = await _userService.GetByIdAsync(123);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfUsers()
        {
            // Arrange
            List<User> users = new List<User>
            {
                new User { UserId = 1, Username = "user1", Email = "user1@example.com" },
                new User { UserId = 2, Username = "user2", Email = "user2@example.com" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<User, object>>[]>())).ReturnsAsync(users);

            // Act
            IEnumerable<User> result = await _userService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<User>>());
            Assert.That(users.Count, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task UpdateAsync_ValidUser_ReturnsTrue()
        {
            // Arrange
            User userToUpdate = new User { UserId = 1, Username = "olduser", Email = "old@example.com" };
            User updatedUser = new User { UserId = 1, Username = "newuser", Email = "new@example.com" };


            _mockRepository.Setup(repo => repo.GetByIdAsync(userToUpdate.UserId)).ReturnsAsync(userToUpdate);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            bool result = await _userService.UpdateAsync(updatedUser);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task DisableAsync_NonExistingUser_ThrowsTaskCanceledException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(() => _userService.DisableAsync(123));
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task SignInAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            string username = "SuperAdmin";
            string password = "password123";

            _mockUserService.Setup(service => service.SignInAsync(username, password)).ReturnsAsync(new User { UserId = 1, Username = username });

            // Act
            var result = await _mockUserService.Object.SignInAsync(username, password);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Username, Is.EqualTo(username));
        }

        [TearDown]
        public void TearDown()
        {
            _signInController.Dispose();
        }
    }
} 