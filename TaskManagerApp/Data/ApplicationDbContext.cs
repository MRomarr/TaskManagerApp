using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaskManagerApp.Models;

namespace TaskManagerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<List> Lists { get; set; }
    }
}
