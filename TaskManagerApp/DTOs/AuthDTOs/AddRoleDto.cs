namespace TaskManagerApp.DTOs.AuthDTOs
{
    public class AddRoleDto
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string UserId { get; set; }
        
    }
}
