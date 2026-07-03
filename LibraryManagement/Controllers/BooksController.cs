using LibraryManagement.DTos.BookDTOS;
using LibraryManagement.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookservice) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await bookservice.GetAllBook();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await bookservice.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookCreateDTO dto)
        {
            var book = await bookservice.CreateBook(dto);
            return Ok(book);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id,BookUpdateDTO dto)
        {
           var book = await bookservice.UpdateBook(id,dto);
           return Ok(book);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await bookservice.DeleteBook(id);
            if(book == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
