using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories.Implementation;

public class BookCopyRepository(AppDbContext context) : GenericRepository<BookCopy>(context), IBookCopyRepository
{
    public async Task<BookCopy> GetAvailabeCopy(int bookId)
    {
        return await _context.BookCopies.FirstOrDefaultAsync(x=> x.BookId == bookId && x.IsAvailable);
    }
}
