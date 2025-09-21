using Microsoft.EntityFrameworkCore;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
namespace Bainah.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _db;
    private readonly DbSet<T> _set;
    public GenericRepository(AppDbContext db) { _db = db; _set = db.Set<T>(); }

    public async Task<T> AddAsync(T entity) { var r = await _set.AddAsync(entity); await _db.SaveChangesAsync(); return r.Entity; }
    public async Task DeleteAsync(int id) { var e = await _set.FindAsync(id); if (e!=null) { _set.Remove(e); await _db.SaveChangesAsync(); } }
    public async Task<T?> GetByIdAsync(int id) => await _set.FindAsync(id);
    public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(_set.AsNoTracking().AsEnumerable());
    public Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T,bool>> predicate) => Task.FromResult(_set.AsNoTracking().Where(predicate).AsEnumerable());
    public async Task<T> UpdateAsync(T entity) { _set.Update(entity); await _db.SaveChangesAsync(); return entity; }
}
