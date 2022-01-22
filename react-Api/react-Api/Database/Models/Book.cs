using System.Collections.Generic;

namespace react_Api.Database.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string FacialImage { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
