namespace api.Domain.Entities
{
    public class BookRating
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Grade { get; set; }
    }
}