using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories.Implementation;

public class MemberRepository(AppDbContext context) : GenericRepository<Member>(context), IMemberRepository
{
    public async Task<Member> GetMemberBorrowHistory(int id)
    {
        return await _context.Members.Include(x => x.BorrowRecords).ThenInclude(x => x.BookCopy).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Member>> GetMemberOverDueMoreThan3()
    {
        return await _context.Members.Where(x => x.BorrowRecords.Count(x => x.ReturnAt != null && x.ReturnAt > x.BorrowedAt.AddDays(10)) > 5).ToListAsync();
    }
}
