using LibraryManagement.DTos.BorrowReturnDTOs;

namespace LibraryManagement.Services.Interface;

public interface IBorrowService
{
    Task<BorrowRecordResponseDto> BorrowBook(BookBorrowDTO dto);
    Task<BorrowRecordResponseDto> ReturnBook(ReturnBookDTO dto);
    Task<IEnumerable<BorrowRecordResponseDto>> GetActiveBorrow();
}
