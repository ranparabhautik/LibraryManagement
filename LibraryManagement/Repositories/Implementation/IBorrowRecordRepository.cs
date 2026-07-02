using LibraryManagement.Model;

namespace LibraryManagement.Repositories.Implementation;

public interface IBorrowRecordRepository
{
    Task<IEnumerable<BorrowRecord>> GetActiveBorrow();
}
