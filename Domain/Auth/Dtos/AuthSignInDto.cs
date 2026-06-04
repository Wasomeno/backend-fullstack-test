using System.ComponentModel.DataAnnotations;

namespace WarehouseSystemTest.Domain.Auth.Dto
{
    public class AuthSignInDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
