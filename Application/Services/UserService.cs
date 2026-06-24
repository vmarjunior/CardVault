using CardVault.Application.DTOs.User;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Interfaces;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;

namespace CardVault.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserQueries _userQueries;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IUserQueries userQueries)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _userQueries = userQueries;
        }

        public async Task<PagedResult<UserViewDTO>> GetAllAsync(UserQueryParameters queryParams)
        {
            return await _userQueries.GetAll(queryParams);
        }

        public async Task<UserViewDTO?> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user?.ToDto();
        }

        public async Task<UserViewDTO> CreateAsync(UserCreateDTO userCreateDTO)
        {
            if (await _userRepository.IsAccountNameTakenAsync(null, userCreateDTO.AccountName))
                throw new InvalidOperationException("This account name is already in use.");

            var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(userCreateDTO.Password);

            var user = new User(
                userCreateDTO.AccountName,
                passwordHash,
                passwordSalt,
                userCreateDTO.Nickname
            );

            var newUser = await _userRepository.CreateAsync(user);
            return newUser.ToDto();
        }

        public async Task UpdateProfileAsync(Guid id, UserUpdateProfileDTO userUpdateProfileDTO)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);

            if (userToUpdate == null)
                throw new InvalidOperationException($"Cannot update user with ID {id} because it was not found.");

            userToUpdate.UpdateProfile(userUpdateProfileDTO.Nickname);

            await _userRepository.UpdateAsync(userToUpdate);
        }

        public async Task UpdateAccountAsync(Guid id, UserUpdateAccountDTO userUpdateAccountDTO)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);

            if (userToUpdate == null)
                throw new InvalidOperationException($"Cannot update user with ID {id} because it was not found.");

            if (await _userRepository.IsAccountNameTakenAsync(id, userUpdateAccountDTO.AccountName))
                throw new InvalidOperationException("This account name is already in use.");

            if (!_passwordHasher.VerifyPassword(userUpdateAccountDTO.CurrentPassword, userToUpdate.PasswordHash, userToUpdate.PasswordSalt))
                throw new ArgumentException("Operation failed. Current password is incorrect.");

            var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(userUpdateAccountDTO.NewPassword);

            userToUpdate.UpdateAccount(userUpdateAccountDTO.AccountName, passwordHash, passwordSalt);

            await _userRepository.UpdateAsync(userToUpdate);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}