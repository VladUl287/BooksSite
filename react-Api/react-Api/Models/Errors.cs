namespace api.Models
{
    public class Errors
    {
        public static readonly Error BookAlreadyExists = new("Книга уже существует.");
        public static readonly Error NotCorrectEmailOrPassword = new("Неверный email или пароль.");
        public static readonly Error UserAlreadyExists = new("Пользователь с таким email уже существует.");
    }
}