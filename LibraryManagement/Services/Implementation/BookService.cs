using AutoMapper;
using LibraryManagement.DTos.BookDTOS;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Interface;

namespace LibraryManagement.Services.Implementation;

public class BookService(IBookRepository _repo, IMapper _mapper,IBookCopyRepository _bookcopyrepo) : IBookService
{
    public async Task<BookResponseDTO> CreateBook(BookCreateDTO dto)
    {
        var book = _mapper.Map<Book>(dto);
        book.CopyIssued = 0;
        await _repo.Create(book);

        for(int i = 1; i <= dto.Stock; i++)
        {
            var copy = new BookCopy
            {
                BookId = book.Id,
                CopyId = $"BOOK-{book.Id}-{i}",
                IsAvailable = true ,
            };

            await _bookcopyrepo.Create(copy);
        }



        return _mapper.Map<BookResponseDTO>(book);
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _repo.GetById(id);
        if (book == null)
        {
            return false;
        }
        if(book is IsDeletable softdel)
        {
            softdel.IsDeletable = true;
            await _repo.Update(book);
            return true;
        }
        else
        {
            _repo.Delete(id);
            return true;
        }
    }

    public async Task<IEnumerable<BookResponseDTO>> GetAllBook()
    {
        var allBooks = await _repo.GetAll();
        return _mapper.Map<IEnumerable<BookResponseDTO>>(allBooks);
    }

    public async Task<BookResponseDTO?> GetBookById(int id)
    {
        var book = await _repo.GetById(id);
        if(book == null) { return null; }
        return _mapper.Map<BookResponseDTO>(book);
    }

    public async Task<BookResponseDTO> UpdateBook(int id ,BookUpdateDTO dto)
    {
        var book = await _repo.GetById(id);
        if (book == null)
            throw new Exception("Book not found.");
        var mappedbook = _mapper.Map(dto,book);
        await _repo.Update(mappedbook);
        return _mapper.Map<BookResponseDTO>(mappedbook);
    }
}
