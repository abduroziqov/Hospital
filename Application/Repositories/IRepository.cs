namespace Application.Repositories
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int Id);
        Task<T> GetByIdAsync(int Id);
    }
}
