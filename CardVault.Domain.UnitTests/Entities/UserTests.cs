using CardVault.Domain.Entities;

namespace CardVault.Domain.UnitTests.Entities
{
    public class UserTests
    {
        private User CreateValidUser()
        {
            return new User(
                "test_account",
                new byte[] { 1, 2, 3 },
                new byte[] { 4, 5, 6 },
                "TestNickname"
            );
        }

        #region Constructor Tests

        [Fact]
        public void Constructor_ShouldCreateUser_WhenAllParametersAreValid()
        {
            // Arrange
            var accountName = "valid_account";
            var nickname = "ValidNickname";
            var hash = new byte[] { 1 };
            var salt = new byte[] { 1 };

            // Act
            var user = new User(accountName, hash, salt, nickname);

            // Assert
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal(accountName, user.AccountName);
            Assert.Equal(nickname, user.Nickname);
            Assert.Equal(hash, user.PasswordHash);
            Assert.Equal(salt, user.PasswordSalt);
            Assert.NotEqual(default, user.Created);
            Assert.Null(user.LastActive);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_ShouldThrowInvalidOperationException_WhenAccountNameIsInvalid(string? invalidAccountName)
        {
            Assert.Throws<InvalidOperationException>(() => new User(
                invalidAccountName!, new byte[] { 1 }, new byte[] { 1 }, "Nickname"
            ));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_ShouldThrowInvalidOperationException_WhenNicknameIsInvalid(string? invalidNickname)
        {
            Assert.Throws<InvalidOperationException>(() => new User(
                "accountName", new byte[] { 1 }, new byte[] { 1 }, invalidNickname!
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidOperationException_WhenPasswordHashIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => new User(
                "accountName", null!, new byte[] { 1 }, "Nickname"
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidOperationException_WhenPasswordSaltIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => new User(
                "accountName", new byte[] { 1 }, null!, "Nickname"
            ));
        }
        #endregion

        #region Method Tests

        [Fact]
        public void UpdateAccount_ShouldUpdateProperties_WhenParametersAreValid()
        {
            // Arrange
            var user = CreateValidUser();
            var newAccountName = "new_account";
            var newHash = new byte[] { 10, 20 };
            var newSalt = new byte[] { 30, 40 };

            // Act
            user.UpdateAccount(newAccountName, newHash, newSalt);

            // Assert
            Assert.Equal(newAccountName, user.AccountName);
            Assert.Equal(newHash, user.PasswordHash);
            Assert.Equal(newSalt, user.PasswordSalt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateAccount_ShouldThrowInvalidOperationException_WhenAccountNameIsInvalid(string? invalidAccountName)
        {
            var user = CreateValidUser();
            Assert.Throws<InvalidOperationException>(() => user.UpdateAccount(invalidAccountName!, new byte[] { 1 }, new byte[] { 1 }));
        }

        [Fact]
        public void UpdateAccount_ShouldThrowArgumentException_WhenPasswordHashIsNull()
        {
            var user = CreateValidUser();
            Assert.Throws<ArgumentException>(() => user.UpdateAccount("acc", null!, new byte[] { 1 }));
        }

        [Fact]
        public void UpdateAccount_ShouldThrowArgumentException_WhenPasswordSaltIsNull()
        {
            var user = CreateValidUser();
            Assert.Throws<ArgumentException>(() => user.UpdateAccount("acc", new byte[] { 1 }, null!));
        }

        [Fact]
        public void UpdateProfile_ShouldUpdateNickname_WhenValid()
        {
            // Arrange
            var user = CreateValidUser();
            var newNickname = "NewNick";

            // Act
            user.UpdateProfile(newNickname);

            // Assert
            Assert.Equal(newNickname, user.Nickname);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateProfile_ShouldThrowInvalidOperationException_WhenNicknameIsInvalid(string? invalidNickname)
        {
            var user = CreateValidUser();
            Assert.Throws<InvalidOperationException>(() => user.UpdateProfile(invalidNickname!));
        }

        [Fact]
        public void UpdateLastTimeActive_ShouldUpdateProperty()
        {
            // Arrange
            var user = CreateValidUser();

            // Act
            user.UpdateLastTimeActive();

            // Assert
            Assert.NotNull(user.LastActive);
        }
        #endregion
    }
}