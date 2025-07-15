namespace TaskManagerApp.DTOs.TaskDTOs
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, 1)]
        public int Status { get; set; }
        [Range(0, 3)]
        public int Priority { get; set; }
        public DateTime? Date { get; set; }
    }
}
