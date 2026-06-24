using CardVault.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.DTOs.User
{
    public record UserCreateDTO
    {
        [Required]
        public string AccountName { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
        
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirmation { get; set; } = default!;

        [Required]
        public string Nickname { get; set; } = default!;
    }
}
