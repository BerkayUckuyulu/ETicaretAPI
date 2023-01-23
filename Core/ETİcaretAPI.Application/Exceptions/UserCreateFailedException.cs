using System;
using System.Runtime.Serialization;

namespace ETİcaretAPI.Application.Exceptions
{
	public class UserCreateFailedException:Exception
	{
		public UserCreateFailedException():base("Kullanıcı oluşturulurken beklenmedik bir hata ile karşılaşıldı.")
		{
		}

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

