
namespace TaskManagerApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<List> lists { get; set; } = new List<List>();
    }
}
