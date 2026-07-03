using AutoMapper;
using LibraryManagement.DTos.BorrowReturnDTOs;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Interface;

namespace LibraryManagement.Services.Implementation;

public class BorrowService(IMapper _mapper,IUnitOfWork uow) : IBorrowService
{
    public async Task<BorrowRecordResponseDto> BorrowBook(BookBorrowDTO dto)
    {
        var member = await uow.MemberUOW.GetById(dto.MemberId);
        if (member == null) { throw new Exception("Member not found"); }

        var bookcopy = await uow.BookCopiesUOW.GetAvailabeCopy(dto.BookId);
        if (bookcopy == null) { throw new Exception("Book not found"); }

        if (!bookcopy.IsAvailable)
        {
            throw new Exception("Book copy is already borrowed.");
        }

        var borrowrecord = new BorrowRecord
        {
            MemberId = dto.MemberId,
            BookCopyId = bookcopy.Id,
            BorrowedAt = DateTime.Now,
            ReturnAt = null
        };
        await uow.BorrowUOW.Create(borrowrecord);
             
        bookcopy.IsAvailable = false;
        await uow.BookCopiesUOW.Update(bookcopy);

        bookcopy.Books.CopyIssued++;
        await uow.BookUOW.Update(bookcopy.Books);

        await uow.SaveChangesAsync();

        var result = await uow.BorrowUOW.GetBorrowRecordsWithDetails(borrowrecord.Id);

        return _mapper.Map<BorrowRecordResponseDto>(result);
    }

    public async Task<IEnumerable<BorrowRecordResponseDto>> GetActiveBorrow()
    {
        var records = await uow.BorrowUOW.GetActiveBorrow();
        return _mapper.Map<IEnumerable< BorrowRecordResponseDto>>(records);
    }

    public async Task<BorrowRecordResponseDto> ReturnBook(ReturnBookDTO dto)
    {
        var borrow = await uow.BorrowUOW.GetBorrowRecordsWithDetails(dto.BorrowRecordId);
        if(borrow == null)
        {
            throw new Exception("Record not found");
        }
        
        if(borrow.ReturnAt != null)
        {
            throw new Exception("Book Already returned");   
        }
        borrow.ReturnAt = DateTime.Now;
        await uow.BorrowUOW.Update(borrow);

        borrow.BookCopy.IsAvailable = true;
        await uow.BookCopiesUOW.Update(borrow.BookCopy);

        borrow.BookCopy.Books.CopyIssued--;
        await uow.BookUOW.Update(borrow.BookCopy.Books);

        await uow.SaveChangesAsync();
        return _mapper.Map<BorrowRecordResponseDto>(borrow);
    }
}
