using CardVault.Application.DTOs.UserCard;

namespace CardVault.Application.Interfaces
{
    public interface IUserCardService
    {
        public Task<UserCardResponseDTO> GetByIdAsync(Guid id);
        public Task<UserCardResponseDTO> ScanCardAsync(Guid userId, CardScanDTO cardScanDTO);
        public Task UpdateCardVirtualStatusAsync(Guid userId, Guid id);
        public Task DeleteAsync(Guid userId, Guid id);
    }
}
