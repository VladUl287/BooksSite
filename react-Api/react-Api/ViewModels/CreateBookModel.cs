using Microsoft.AspNetCore.Http;

namespace api.ViewModels
{
    public class CreateBookModel : BookModel
    {
        public IFormFile Image { get; set; }
    }
}