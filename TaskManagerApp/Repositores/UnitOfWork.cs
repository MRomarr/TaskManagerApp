using TaskManagerApp.Data;

namespace TaskManagerApp.Repositores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context; 
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context, _context.Set<T>());
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
