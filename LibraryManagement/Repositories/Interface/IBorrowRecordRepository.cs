using LibraryManagement.Model;

namespace LibraryManagement.Repositories.Interface;

public interface IBorrowRecordRepository:IGenericRepository<BorrowRecord>
{
    Task<IEnumerable<BorrowRecord>> GetActiveBorrow();
}
