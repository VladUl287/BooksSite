using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace react_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IEnumerable<BookModel> Get()
        {
            return Enumerable.Range(1, 10).Select(index => new BookModel
            {
                Name = $"Book {index}",
                Author = $"Author {index}"
            });
        }
    }
}