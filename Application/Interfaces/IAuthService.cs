using CardVault.Application.DTOs.Auth;

namespace CardVault.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(AuthRequestDTO authRequest);
    }
}
