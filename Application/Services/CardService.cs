using CardVault.Application.DTOs.Card;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Interfaces;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;

namespace CardVault.Application.Services
{
    public class CardService(ICardRepository cardRepository, 
                            ICardQueries cardQueries, 
                            ICardExternalService cardExternalService) : ICardService
    {
        public async Task<PagedResult<CardResponseDTO>> GetAllAsync(CardQueryParameters queryParams)
        {
            return await cardQueries.GetAll(queryParams);
        }

        public async Task<CardResponseDTO> GetByIdAsync(Guid id)
        {
            var card = await cardRepository.GetByIdAsync(id);
            if (card == null)
                throw new InvalidOperationException($"Card with id '{id}' not found.");

            return card.ToDto();
        }

        public async Task<Card> GetOrCreateAsync(string name, string setCode)
        {
            var card = await cardRepository.GetByNameAndSetAsync(name, setCode);

            if (card != null)
                return card;

            card = await cardExternalService.GetCardAsync(name, setCode);

            if (card == null)
                throw new InvalidOperationException(
                    "Card could not be found.");

            await cardRepository.AddAsync(card);

            return card;
        }

        public async Task UpdateCardPriceAsync(Guid id, decimal price)
        {
            var card = await cardRepository.GetByIdAsync(id);
            if (card == null)
                throw new InvalidOperationException($"Card with id '{id}' not found.");

            card.UpdateCardPrice(price);
            await cardRepository.UpdateAsync(card);
        }

        public async Task DeleteAsync(Guid id)
        {
            var card = await cardRepository.GetByIdAsync(id);
            if (card == null)
                throw new InvalidOperationException($"Card with id '{id}' not found.");

            await cardRepository.RemoveAsync(card);
        }
    }
}
