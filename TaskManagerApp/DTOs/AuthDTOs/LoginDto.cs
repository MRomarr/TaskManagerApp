namespace TaskManagerApp.DTOs.AuthDTOs
{
    public class LoginDto
    {
        [Required]
        public string EmailOrUserName { get; set; }
        [Required]
        public string Password { get; set; }   
    }
}
