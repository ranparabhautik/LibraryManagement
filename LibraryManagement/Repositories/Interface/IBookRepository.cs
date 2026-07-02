using LibraryManagement.Model;
using LibraryManagement.Repositories.Implementation;

namespace LibraryManagement.Repositories.Interface;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<Book> GetBookWithCopies(int id);
    Task<IEnumerable<Book>> SearchBook(string keyword);
}
