namespace TaskManagerApp.Models
{
    public class Task
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? Date { get; set; }
        public TaskStatus Status { get; set; } = (int)TaskStatus.Pending;
        public TaskPriority Priority { get; set; } = (int)TaskPriority.none;
        public string ListId { get; set; }
        public List List { get; set; }


    }
    public enum TaskStatus
    {
        Pending=0,
        Completed=1,
    }
    public enum TaskPriority
    {
        none=0,
        Low = 1,
        Medium = 2,
        High = 3
    }
}
