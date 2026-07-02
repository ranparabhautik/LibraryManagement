using LibraryManagement.DTos.BookDTOS;

namespace LibraryManagement.Services.Interface;

public interface IBookService
{
    Task<BookResponseDTO> CreateBook(BookCreateDTO dto);
    Task<BookResponseDTO> UpdateBook(int id,BookUpdateDTO dto);
    Task<bool> DeleteBook(int id);
    Task<IEnumerable< BookResponseDTO>> GetAllBook();
    Task<BookResponseDTO?> GetBookById(int id);
}
