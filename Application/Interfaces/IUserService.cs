using CardVault.Application.DTOs.User;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;
using CardVault.Application.Services;
using CardVault.Domain.Enums;

namespace CardVault.Application.Interfaces
{
    public interface IUserService
    {
        public Task<PagedResult<UserResponseDTO>> GetAllAsync(UserQueryParameters queryParameters);
        public Task<UserResponseDTO?> GetByIdAsync(Guid id);
        public Task<UserResponseDTO> CreateAsync(UserCreateDTO userCreateDTO);
        public Task UpdateProfileAsync(Guid currentUserId, Guid id, UserUpdateProfileDTO userUpdateDTO);
        public Task UpdateAccountAsync(Guid currentUserId, Guid id, UserUpdateAccountDTO userUpdateDTO);
        public Task DeleteAsync(Guid currentUserId, Guid id);
    }
}
