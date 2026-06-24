using CardVault.Application.DTOs.User;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;

namespace CardVault.Application.Queries
{
    public interface IUserQueries
    {
        public Task<PagedResult<UserViewDTO>> GetAll(UserQueryParameters queryParams);
    }
}
