using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.DTOs.User
{
    public record UserUpdateAccountDTO
    {
        [Required]
        public string AccountName { get; set; } = default!;

        [Required]
        public string CurrentPassword { get; set; } = default!;

        [Required]
        public string NewPassword { get; set; } = default!;

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPasswordConfirmation { get; set; } = default!;
    }
}
