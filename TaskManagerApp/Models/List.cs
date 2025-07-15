namespace TaskManagerApp.Models
{
    public class List
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
