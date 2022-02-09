using System.Collections.Generic;

namespace api.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PagesCount { get; set; }
        public string Image { get; set; }
        public ICollection<BookAuthor> BooksAuthors { get; set; }
        public ICollection<BookRating> BooksRatings { get; set; }
    }
}