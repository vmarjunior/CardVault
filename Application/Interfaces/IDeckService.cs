using CardVault.Application.DTOs.Deck;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;

namespace CardVault.Application.Interfaces
{
    public interface IDeckService
    {
        public Task<PagedResult<DeckListResponseDTO>> GetAllAsync(DeckQueryParameters queryParameters);
        public Task<DeckResponseDTO> GetByIdAsync(Guid id);
        public Task<ICollection<DeckListResponseDTO>> GetByUserIdAsync(Guid userId);
        public Task<DeckResponseDTO> CreateAsync(Guid userId, DeckCreateDTO deckCreateDTO);
        public Task UpdateAsync(Guid userId, Guid id, DeckUpdateDTO deckUpdateDto);
        public Task DeleteAsync(Guid userId, Guid id);
    }
}
