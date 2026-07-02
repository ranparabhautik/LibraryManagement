using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories.Implementation;

public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    public async Task<Book> GetBookWithCopies(int id)
    {
        return await _context.Books.Include(x => x.Copies).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Book>> SearchBook(string keyword)
    {
        return await _context.Books.Where(x=> x.BookName.Contains(keyword)).ToListAsync();
    }
}
