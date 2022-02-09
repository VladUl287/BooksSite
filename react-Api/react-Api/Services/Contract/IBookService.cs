using OneOf;
using api.Models;
using api.ViewModels;
using api.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace api.Services.Contract
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllBooks(int size, int page);
        Task<BookModel> GetBook(int id);
        Task<string> GetImage(int id, bool facial);
        Task<OneOf<Book, BookAlreadyExists>> CreateBook(CreateBookModel bookModel);
        Task<BookRating> UpdateRatingBook(UpdateRatingModel ratingModel);
        Task DeleteBook(int id);
    }
}