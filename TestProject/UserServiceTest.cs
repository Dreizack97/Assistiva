using BLL.Implementation;
using BLL.Interfaces;
using DAL.Interfaces;
using Entity;
using Moq;
using NUnit.Framework.Legacy;

namespace BLL.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IGenericRepository<User>> _mockRepository;
        private Mock<IEmailService> _mockEmailService;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericRepository<User>>();
            _mockEmailService = new Mock<IEmailService>();
            _userService = new UserService(_mockRepository.Object, _mockEmailService.Object);
        }

        [Test]
        public async Task CreateAsync_ValidUser_ReturnsCreatedUser()
        {
            // Arrange
            var newUser = new User { Username = "JSilva", Email = "dreizack97@gmail.com" };
            var createdUser = new User { UserId = 1, Username = "JSilva", Email = "dreizack97@gmail.com" };

            _mockRepository.Setup(repo => repo.GetByFilterAsync(u => u.Username == newUser.Username)).ReturnsAsync((User?)null);
            _mockRepository.Setup(repo => repo.AddAsync(newUser)).ReturnsAsync(createdUser);
            // _mockEmailService.Setup(email => email.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _userService.CreateAsync(newUser);

            // Assert
            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(createdUser.UserId, result.UserId);
            ClassicAssert.AreEqual(createdUser.Username, result.Username);

            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);

            //_mockEmailService.Verify(email => email.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}