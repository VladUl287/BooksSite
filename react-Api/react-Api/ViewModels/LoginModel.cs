using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class LoginModel
    {
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}