using LibraryManagement.Model;

namespace LibraryManagement.Repositories.Interface;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(int id);
}
