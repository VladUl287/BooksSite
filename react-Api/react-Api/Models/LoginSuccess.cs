namespace api.Models
{
    public class LoginSuccess
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}