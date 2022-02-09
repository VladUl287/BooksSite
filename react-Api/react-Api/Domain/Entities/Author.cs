using System.Collections.Generic;

namespace api.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BookAuthor> BooksAuthors { get; set; }
    }
}