using LibraryManagement.Model;

namespace LibraryManagement.Repositories.Interface;

public interface IBookCopyRepository:IGenericRepository<BookCopy>
{
    Task<BookCopy> GetAvailabeCopy(int bookId);
}
