using CardVault.Application.DTOs.CardSet;
using CardVault.Domain.Entities;

namespace CardVault.Application.Mappers
{
    public static class CardSetMapper
    {
        public static CardSetResponseDTO ToDto(this CardSet cardSet)
        {
            return new CardSetResponseDTO
            {
                Id = cardSet.Id,
                Code = cardSet.Code,
                Name = cardSet.Name,
                ImageUrl = cardSet.ImageUrl,
                ReleaseDate = cardSet.ReleaseDate
            };
        }
    }
}
