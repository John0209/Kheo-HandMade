using DataAccess.Entites;

namespace ClassLibrary1.Interface.IRepositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAsync();
    void Update(T entity);
    void Delete(T entity);
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id, bool disableTracking = false);
    public Task AddRangeAsync(IEnumerable<T> entity);
}