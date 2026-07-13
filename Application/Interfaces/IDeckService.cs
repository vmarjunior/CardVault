using CardVault.Application.DTOs.Deck;

namespace CardVault.Application.Interfaces
{
    public interface IDeckService
    {
        public Task<DeckResponseDTO> GetByIdAsync(Guid id);
        public Task<ICollection<DeckListResponseDTO>> GetByUserIdAsync(Guid userId);
        public Task<DeckResponseDTO> CreateAsync(Guid userId, DeckCreateDTO deckCreateDTO);
        public Task UpdateAsync(Guid userId, Guid id, DeckUpdateDTO deckUpdateDto);
        public Task DeleteAsync(Guid userId, Guid id);
    }
}
