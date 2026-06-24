using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.DTOs.User
{
    public record UserUpdateProfileDTO
    {
        [Required]
        public string Nickname { get; set; } = default!;
    }
}
