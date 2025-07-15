namespace TaskManagerApp.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        System.Threading.Tasks.Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
