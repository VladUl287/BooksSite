using api.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class UpdateRatingModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        [MinMax]
        public int Grade { get; set; }
    }
}