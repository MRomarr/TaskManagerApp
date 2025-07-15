namespace TaskManagerApp.DTOs.TaskDTOs
{
    public class CreateTaskDto
    {
        public string ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, 1)]
        public int Status { get; set; } = 0;
        [Range(0, 3)]
        public int Priority { get; set; } = 0;
        public DateTime? Date { get; set; }
    }
}
