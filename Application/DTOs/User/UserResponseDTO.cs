using CardVault.Domain.Enums;

namespace CardVault.Application.DTOs.User
{
    public record UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = default!;
        public int DeckCount { get; set; } = default!;
        public int CardCount { get; set; } = default!;
        public DateTime Created { get; set; }
        public DateTime? LastActive { get; set; } = default;
    }
}
