using CardVault.Application.DTOs.Card;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;

namespace CardVault.Application.Interfaces
{
    public interface ICardService
    {
        public Task<PagedResult<CardResponseDTO>> GetAllAsync(CardQueryParameters queryParams);
        public Task<CardResponseDTO> GetByIdAsync(Guid id);
        public Task<Card> GetOrCreateAsync(string name, string setCode);
        public Task UpdateCardPriceAsync(Guid id, decimal price);
        public Task DeleteAsync(Guid id);
    }
}
