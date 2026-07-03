namespace LibraryManagement.Repositories.Interface;

public interface IUnitOfWork:IDisposable
{
    IBookCopyRepository BookCopiesUOW { get;  }
    IBookRepository BookUOW { get; }
    IMemberRepository MemberUOW { get; }
    IBorrowRecordRepository BorrowUOW { get; }
    Task<int> SaveChangesAsync();
}
