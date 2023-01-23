using System;
namespace ETİcaretAPI.Application.Exceptions
{
    public class NotFoundUserExeption : Exception
    {
        public NotFoundUserExeption():base("Kullanıcı adı veya şifre hatalı.")
        {
        }

        public NotFoundUserExeption(string? message) : base(message)
        {
        }

        public NotFoundUserExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

