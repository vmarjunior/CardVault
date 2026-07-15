using CardVault.Application.DTOs.Deck;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Interfaces;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;

namespace CardVault.Application.Services
{
    public class DeckService(IDeckRepository deckRepository, IDeckQueries deckQueries, IUserRepository userRepository) : IDeckService
    {
        public async Task<PagedResult<DeckListResponseDTO>> GetAllAsync(DeckQueryParameters queryParameters)
        {
            return await deckQueries.GetAll(queryParameters);
        }

        public async Task<DeckResponseDTO> GetByIdAsync(Guid id)
        {
            var deck = await deckRepository.GetByIdAsync(id);
            if (deck == null)
                throw new InvalidOperationException("Deck not found.");

            return deck.ToDto();
        }

        public async Task<ICollection<DeckListResponseDTO>> GetByUserIdAsync(Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            var decks = await deckRepository.GetByUserIdAsync(userId);
            return decks.Select(x => x.ToListDto()).ToList();
        }

        public async Task<DeckResponseDTO> CreateAsync(Guid userId, DeckCreateDTO deckCreateDTO)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            var deck = new Deck(deckCreateDTO.Name, deckCreateDTO.Type, user, deckCreateDTO.IsPrivate);
            await deckRepository.AddAsync(deck);

            return deck.ToDto();
        }

        public async Task UpdateAsync(Guid userId, Guid id, DeckUpdateDTO deckUpdateDto)
        {
            var deck = await deckRepository.GetByIdAsync(id);
            if (deck == null)
                throw new InvalidOperationException("Deck not found.");

            AuthorizationHelper.EnsureResourceOwner(userId, deck.User.Id);

            deck.UpdateDeckInformation(deckUpdateDto.Name, deckUpdateDto.Type, deckUpdateDto.IsPrivate);

            await deckRepository.UpdateAsync(deck);
        }

        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var deck = await deckRepository.GetByIdAsync(id);
            if (deck == null)
                throw new InvalidOperationException("Deck not found.");

            AuthorizationHelper.EnsureResourceOwner(userId, deck.User.Id);

            await deckRepository.RemoveAsync(deck);
        }
    }
}
