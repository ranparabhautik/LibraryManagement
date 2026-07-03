using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories.Implementation;

public class BorrowRecordRepository(AppDbContext context) : GenericRepository<BorrowRecord>(context), IBorrowRecordRepository
{
    public async Task<IEnumerable<BorrowRecord>> GetActiveBorrow()
    {
        return await _context.BorrowRecords.Where(x => x.ReturnAt == null).Include(x => x.Member).Include(x => x.BookCopy).ToListAsync();
    }

    public async Task<BorrowRecord?> GetBorrowRecordsWithDetails(int id)
    {
        return await _context.BorrowRecords.Include(x => x.Member).Include(x => x.BookCopy).ThenInclude(x => x.Books).FirstOrDefaultAsync(x=> x.Id == id);
    }
}
