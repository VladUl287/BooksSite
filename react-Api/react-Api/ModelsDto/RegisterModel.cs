using System.ComponentModel.DataAnnotations;

namespace react_Api.Models
{
    public class RegisterModel: LoginModel
    {
        [Required]
        [MaxLength(150)]
        public string Login { get; set; }
    }
}
