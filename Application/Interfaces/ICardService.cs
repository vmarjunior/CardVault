using CardVault.Application.DTOs.Card;

namespace CardVault.Application.Interfaces
{
    public interface ICardService
    {
        public Task<CardResponseDTO> GetByIdAsync(Guid id);
        public Task UpdateCardPriceAsync(Guid id, decimal price);
        public Task DeleteAsync(Guid id);
    }
}
