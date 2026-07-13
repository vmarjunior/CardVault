using CardVault.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.DTOs.Deck
{
    public class DeckCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        public DeckType Type { get; set; }

        public bool IsPrivate { get; set; }
    }
}
