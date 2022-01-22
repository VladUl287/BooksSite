using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react_Api.Database.Models;
using System.Collections.Generic;
using react_Api.Database.Interfaces;

namespace react_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {

            return Array.Empty<Book>();
        }
    }
}
