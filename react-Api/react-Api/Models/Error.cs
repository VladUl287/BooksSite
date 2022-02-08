namespace api.Models
{
    public class Error
    {
        public Error(string message)
        {
            ErrorMessage = message;
        }

        public string ErrorMessage { get; }
    }
}