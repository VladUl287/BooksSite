using api.Domain.Entities;

namespace api.Database.Models
{
    public class Token : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string RefreshToken { get; set; }
    }
}