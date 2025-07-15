namespace TaskManagerApp.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveAsync();
    }
    
}
