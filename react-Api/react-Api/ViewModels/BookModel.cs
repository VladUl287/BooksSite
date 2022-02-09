namespace api.ViewModels
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public int PagesCount { get; set; }
        public AuthorModel[] Authors { get; set; } 
    }
}