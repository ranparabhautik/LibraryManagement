using AutoMapper;
using LibraryManagement.DTos.BookDTOS;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Interface;

namespace LibraryManagement.Services.Implementation;

public class BookService(IUnitOfWork uow, IMapper _mapper) : IBookService
{
    public async Task<BookResponseDTO> CreateBook(BookCreateDTO dto)
    {
        var book = _mapper.Map<Book>(dto);
        book.CopyIssued = 0;
        await uow.BookUOW.Create(book);
        await uow.SaveChangesAsync();

        for (int i = 1; i <= dto.Stock; i++)
        {
            var copy = new BookCopy
            {
                BookId = book.Id,
                CopyId = $"BOOK-{book.Id}-{i}",
                IsAvailable = true,
            };

            //await _bookcopyrepo.Create(copy);
            await uow.BookCopiesUOW.Create(copy);
        }

        await uow.SaveChangesAsync();

        return _mapper.Map<BookResponseDTO>(book);
    }

    public async Task<bool> DeleteBook(int id)
    {
        //var book = await _repo.GetById(id);
        var book = await uow.BookUOW.GetById(id);
        if (book == null)
        {
            return false;
        }
        if (book is IsDeletable softdel)
        {
            softdel.IsDeletable = true;
            //await _repo.Update(book);
            await uow.BookUOW.Update(book);
            await uow.SaveChangesAsync();
            return true;
        }
        else
        {
            //_repo.Delete(id);
            await uow.BookUOW.Delete(id);
            await uow.SaveChangesAsync();
            return true;
        }
    }

    public async Task<IEnumerable<BookResponseDTO>> GetAllBook()
    {
        //var allBooks = await _repo.GetAll();
        var allBooks = await uow.BookUOW.GetAll();
        return _mapper.Map<IEnumerable<BookResponseDTO>>(allBooks);
    }

    public async Task<BookResponseDTO?> GetBookById(int id)
    {
        //var book = await _repo.GetById(id);
        var book = await uow.BookUOW.GetById(id);
        if (book == null) { return null; }
        return _mapper.Map<BookResponseDTO>(book);
    }

    public async Task<BookResponseDTO> UpdateBook(int id, BookUpdateDTO dto)
    {
        //var book = await _repo.GetById(id);
        var book = await uow.BookUOW.GetById(id);
        if (book == null)
            throw new Exception("Book not found.");
        var mappedbook = _mapper.Map(dto, book);
        //await _repo.Update(mappedbook);
        await uow.BookUOW.Update(mappedbook);
        await uow.SaveChangesAsync();
        return _mapper.Map<BookResponseDTO>(mappedbook);
    }
}
