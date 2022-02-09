using api.Models;
using api.Services.Contract;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace react_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<BookModel>> GetAll([FromQuery] int size, [FromQuery] int page)
        {
            var books = await bookService.GetAllBooks(size, page);

            return books;
        }

        [HttpGet("{id}")]
        public async Task<BookModel> Get([FromRoute] int id)
        {
            var book = await bookService.GetBook(id);
            book.Image = $"{Request.Scheme}://{Request.Host}/api/book/image/{id}";

            return book;
        }

        [HttpGet("image/{id}")]
        public async Task<PhysicalFileResult> GetImage([FromRoute] int id, [FromQuery] bool facial = true)
        {
            var path = await bookService.GetImage(id, facial);

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookModel bookModel)
        {
            var result = await bookService.CreateBook(bookModel);

            return result.Match<IActionResult>(
                book => CreatedAtAction(nameof(Create), book),
                faild => BadRequest(Errors.BookAlreadyExists)
            );
        }

        [HttpPatch("rating")]
        public async Task<IActionResult> UpdateRating(UpdateRatingModel updateRatingModel)
        {


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await bookService.DeleteBook(id);

            return NoContent();
        }
    }
}