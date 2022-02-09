using api.Database;
using api.Domain.Entities;
using api.Services.Contract;
using api.ViewModels;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace api.Services
{
    public class BookService : IBookService
    {
        private readonly string path;
        private readonly DatabaseContext dbContext;

        public BookService(DatabaseContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;

            path = webHostEnvironment.ContentRootPath + @"\Files\";
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks(int size, int page)
        {
            var query = from book in dbContext.Books
                        select new BookModel
                        {
                            Name = book.Name
                        };

            if (size > 0 && page > 0)
            {
                int skip = size * (page - 1);
                query = query
                    .Skip(skip)
                    .Take(size);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BookModel> GetBook(int id)
        {
            var query = from book in dbContext.Books
                        where book.Id == id
                        select new BookModel
                        {
                            Name = book.Name,
                            Description = book.Description,
                            PagesCount = book.PagesCount,
                            Rating = book.BooksRatings
                                .Where(e => e.BookId == id)
                                .Sum(e => e.Grade) / book.BooksRatings
                                                       .Where(e => e.BookId == id)
                                                       .Count()
                        };

            return await query
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<OneOf<Book, BookAlreadyExists>> CreateBook(CreateBookModel bookModel)
        {
            var exists = await dbContext.Books.AnyAsync(e => e.Name == bookModel.Name);

            if (exists)
            {
                return new BookAlreadyExists();
            }

            var image = bookModel.Image;

            using var img = await Image.LoadAsync(image.OpenReadStream());

            img.Save($@"{path}\Images\{image.FileName}", new JpegEncoder());

            img.Mutate(op => op.Resize(144, 256));

            img.Save($@"{path}\FacialImage\{image.FileName}", new JpegEncoder());

            var book = new Book
            {
                Name = bookModel.Name,
                Description = bookModel.Description,
                PagesCount = bookModel.PagesCount,
                Image = bookModel.Image.FileName
            };

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            await dbContext.Books.AddAsync(book);

            await dbContext.SaveChangesAsync();

            var bookAuthors = new List<BookAuthor>(bookModel.Authors.Length);
            for (int i = 0; i < bookModel.Authors.Length; i++)
            {
                bookAuthors.Add(new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = bookModel.Authors[i].Id
                });
            }

            await dbContext.BooksAuthors.AddRangeAsync(bookAuthors);
            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return book;
        }

        public Task DeleteBook(int id)
        {
            return dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM [Books] WHERE [Id] = {id}");
        }

        public async Task<BookRating> UpdateRatingBook(UpdateRatingModel ratingModel)
        {
            var bookRating = await dbContext.BooksRatings.AddAsync(new BookRating
            {
                BookId = ratingModel.BookId,
                UserId = ratingModel.UserId,
                Grade = ratingModel.Grade
            });

            return bookRating.Entity;
        }

        public async Task<string> GetImage(int id, bool facial)
        {
            var query = from book in dbContext.Books
                        where book.Id == id
                        select book.Name;

            var name = await query.FirstOrDefaultAsync();

            string p = "";

            if (facial)
            {
                p = path + "FacialImage\\" + name; 
            }
            else
            {
                p = path + "Image\\" + name;
            }

            return p;
        }
    }
}