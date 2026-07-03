using LibraryManagement.Data;
using LibraryManagement.Repositories.Interface;

namespace LibraryManagement.Repositories.Implementation;

public class UnitOfWork(AppDbContext context,IBookCopyRepository BookCopyRepo,IBookRepository BookRepo,IMemberRepository MemberRepo,IBorrowRecordRepository BorrowRepo) : IUnitOfWork
{
    public IBookCopyRepository BookCopiesUOW { get; } = BookCopyRepo;

    public IBookRepository BookUOW { get; } = BookRepo;

    public IMemberRepository MemberUOW { get; } = MemberRepo;

    public IBorrowRecordRepository BorrowUOW { get; } = BorrowRepo;

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
