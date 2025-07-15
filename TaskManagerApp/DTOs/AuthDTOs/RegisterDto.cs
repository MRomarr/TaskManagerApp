
namespace TaskManagerApp.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
        
    }
}
