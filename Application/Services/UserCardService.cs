using CardVault.Application.DTOs.UserCard;
using CardVault.Application.Interfaces;
using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;

namespace CardVault.Application.Services
{
    public class UserCardService(
        IUserCardRepository userCardRepository,
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository,
        ICardExternalService cardExternalService) : IUserCardService
    {
        public async Task<UserCardResponseDTO> GetByIdAsync(Guid id)
        {
            var userCard = await userCardRepository.GetByIdAsync(id);
            if (userCard == null)
                throw new InvalidOperationException("Card not found.");

            var userCardDTO = new UserCardResponseDTO
            {
                Id = userCard.Id,
                Name = userCard.Card.Name,
                Price = userCard.Card.Price
            };

            return userCardDTO;
        }

        public async Task<UserCardResponseDTO> ScanCardAsync(Guid userId, CardScanDTO cardScanDTO)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            Card? card = await cardRepository.GetByNameAndSetAsync(cardScanDTO.Name, cardScanDTO.SetCode);

            if (card == null)
            {
                card = await cardExternalService.GetCardAsync(cardScanDTO.Name, cardScanDTO.SetCode);

                if (card == null)
                    throw new InvalidOperationException("Card could not be found in Scryfall API.");

                await cardRepository.AddAsync(card);
            }

            Deck? deck = null;
            if (cardScanDTO.DeckId.HasValue)
            {
                deck = await deckRepository.GetByIdAsync(cardScanDTO.DeckId.Value);
            }

            var newUserCard = new UserCard(user, card, deck);
            var savedCard = await userCardRepository.AddAsync(newUserCard);

            return new UserCardResponseDTO
            {
                Id = savedCard.Id,
                Name = savedCard.Card.Name,
                CardSet = savedCard.Card.Set.Name,
                Price = savedCard.Card.Price
            };
        }

        public async Task UpdateCardVirtualStatusAsync(Guid userId, Guid id)
        {
            var existingCard = await userCardRepository.GetByIdAsync(id);

            if (existingCard == null)
                throw new InvalidOperationException("Card not found.");

            AuthorizationHelper.EnsureResourceOwner(userId, existingCard.User.Id);

            existingCard.ChangeVirtualStatus();

            await userCardRepository.UpdateAsync(existingCard);
        }

        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var existingCard = await userCardRepository.GetByIdAsync(id);

            if (existingCard == null)
                throw new InvalidOperationException("Card not found.");

            AuthorizationHelper.EnsureResourceOwner(userId, existingCard.User.Id);

            await userCardRepository.RemoveAsync(existingCard);
        }
    }
}