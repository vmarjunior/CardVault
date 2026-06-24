using CardVault.Application.DTOs.User;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;
using CardVault.Application.Services;
using CardVault.Domain.Enums;

namespace CardVault.Application.Interfaces
{
    public interface IUserService
    {
        public Task<PagedResult<UserViewDTO>> GetAllAsync(UserQueryParameters queryParameters);
        public Task<UserViewDTO?> GetByIdAsync(Guid id);
        public Task<UserViewDTO> CreateAsync(UserCreateDTO userCreateDTO);
        public Task UpdateProfileAsync(Guid id, UserUpdateProfileDTO userUpdateDTO);
        public Task UpdateAccountAsync(Guid id, UserUpdateAccountDTO userUpdateDTO);
        public Task DeleteAsync(Guid id);
    }
}
