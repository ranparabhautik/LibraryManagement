using AutoMapper;
using LibraryManagement.DTos.BorrowReturnDTOs;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Interface;

namespace LibraryManagement.Services.Implementation;

public class BorrowService(IMapper _mapper, IBorrowRecordRepository _borrowRepo, IBookRepository _bookRepo, IMemberRepository _memberRepo, IBookCopyRepository _copyRepo) : IBorrowService
{
    public async Task<BorrowRecordResponseDto> BorrowBook(BookBorrowDTO dto)
    {
        var member = await _memberRepo.GetById(dto.MemberId);
        if (member == null) { throw new Exception("Member not found"); }

        var bookcopy = await _copyRepo.GetBoolCopywithBook(dto.BookCopyId);
        if (bookcopy == null) { throw new Exception("Book not found"); }

        if (!bookcopy.IsAvailable)
        {
            throw new Exception("Book copy is already borrowed.");
        }

        var borrowrecord = new BorrowRecord
        {
            MemberId = dto.MemberId,
            BookCopyId = dto.BookCopyId,
            BorrowedAt = DateTime.Now,
            ReturnAt = null
        };
        await _borrowRepo.Create(borrowrecord);

        bookcopy.IsAvailable = false;
        await _copyRepo.Update(bookcopy);

        bookcopy.Books.CopyIssued++;
        await _bookRepo.Update(bookcopy.Books);

        return _mapper.Map<BorrowRecordResponseDto>(borrowrecord);

    }

    public Task<IEnumerable<BorrowRecordResponseDto>> GetActiveBorrow()
    {
        throw new NotImplementedException();
    }

    public Task<BorrowRecordResponseDto> ReturnBook(ReturnBookDTO dto)
    {
        throw new NotImplementedException();
    }
}
