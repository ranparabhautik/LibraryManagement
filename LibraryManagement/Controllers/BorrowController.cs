using LibraryManagement.DTos.BorrowReturnDTOs;
using LibraryManagement.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController(IBorrowService borrowService) : ControllerBase
    {
        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook(BookBorrowDTO dto)
        {
            var result = await borrowService.BorrowBook(dto);
            return Ok(result);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(ReturnBookDTO dto)
        {
            var result = await borrowService.ReturnBook(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveBorrow()
        {
            var result = await borrowService.GetActiveBorrow();
            return Ok(result);
        }
    }
}
