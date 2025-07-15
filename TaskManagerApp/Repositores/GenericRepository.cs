

namespace TaskManagerApp.Repositores
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async System.Threading.Tasks.Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


    }
}
