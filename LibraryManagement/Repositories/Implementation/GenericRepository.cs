using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories.Implementation;


public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbset;
    protected readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbset = context.Set<T>();
    }
    
    public async Task Create(T entity)
    {
        await _dbset.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existing = await _dbset.FindAsync(id);
        _dbset.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbset.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        var existing = await _dbset.FindAsync(id);
        return existing;
    }

    public async Task Update(T entity)
    {
         _dbset.Update(entity);
        await _context.SaveChangesAsync();

    }
}
