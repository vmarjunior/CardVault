using CardVault.Application.DTOs.User;
using CardVault.Domain.Entities;

namespace CardVault.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDTO ToDto(this User user)
        {
            return new UserResponseDTO
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
