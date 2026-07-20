using CardVault.Application.DTOs.User;
using CardVault.Application.Interfaces;
using CardVault.Application.Queries;
using CardVault.Application.Services;
using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CardVault.UnitTests.Users
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IPasswordHasher> _passwordHasher;
        private readonly Mock<IUserQueries> _userQueries;

        private readonly UserService _service;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _passwordHasher = new Mock<IPasswordHasher>();
            _userQueries = new Mock<IUserQueries>();

            _service = new UserService(
                _userRepository.Object,
                _passwordHasher.Object,
                _userQueries.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ShouldFail_WhenAccountNameAlreadyExists()
        {
            // Arrange
            var request = new UserCreateDTO
            {
                AccountName = "existingUser",
                Password = "Password123",
                Nickname = "John"
            };

            _userRepository
                .Setup(x => x.IsAccountNameTakenAsync(null, "existingUser"))
                .ReturnsAsync(true);

            // Act
            Func<Task> action = async () =>
                await _service.CreateAsync(request);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("This account name is already in use.");
        }

        [Fact]
        public async Task UpdateAccountAsync_ShouldFail_WhenCurrentPasswordIsIncorrect()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var storedHash = new byte[] { 1, 2, 3 };
            var storedSalt = new byte[] { 4, 5, 6 };

            var user = new User(
                "oldAccount",
                storedHash,
                storedSalt,
                "nickname"
            );

            _userRepository
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(user);

            _userRepository
                .Setup(x => x.IsAccountNameTakenAsync(userId, "newAccount"))
                .ReturnsAsync(false);

            _passwordHasher
                .Setup(x => x.VerifyPassword(
                    "wrongPassword",
                    storedHash,
                    storedSalt))
                .Returns(false);

            var dto = new UserUpdateAccountDTO
            {
                AccountName = "newAccount",
                CurrentPassword = "wrongPassword",
                NewPassword = "newPassword"
            };

            // Act
            Func<Task> action = async () =>
                await _service.UpdateAccountAsync(
                    userId,
                    userId,
                    dto);

            // Assert
            await action.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("Operation failed. Current password is incorrect.");

            _userRepository.Verify(
                x => x.UpdateAsync(It.IsAny<User>()),
                Times.Never);
        }
    }
}
