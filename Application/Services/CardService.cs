using CardVault.Application.DTOs.Card;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Interfaces;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Repositories;

namespace CardVault.Application.Services
{
    public class CardService(ICardRepository cardRepository, ICardQueries cardQueries) : ICardService
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
