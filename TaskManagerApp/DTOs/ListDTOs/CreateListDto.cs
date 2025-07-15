namespace TaskManagerApp.DTOs.ListDTOs
{
    public class CreateListDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; }
    }
}
