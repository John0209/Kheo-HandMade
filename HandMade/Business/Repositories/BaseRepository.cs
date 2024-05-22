using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> DbSet;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    public virtual async Task<List<T>> GetAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual void Update(T entity)
    {
      DbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public virtual async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual async Task<T?> GetByIdAsync(int id, bool disableTracking = false)
    {
        IQueryable<T> query = DbSet;

        if (disableTracking)
        {
            query.AsNoTracking();
        }

        return await query.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
}