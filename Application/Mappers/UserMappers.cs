using CardVault.Application.DTOs.User;
using CardVault.Domain.Entities;

namespace CardVault.Application.Mappers
{
    public static class UserMappers
    {
        public static UserViewDTO ToDto(this User user)
        {
            return new UserViewDTO
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Created = user.Created,
                DeckCount = user.Decks.Count,
                CardCount = user.UserCards.Count,
                LastActive = user.LastActive,
            };
        }
    }
}
