using System;
namespace ETİcaretAPI.Application.Abstraction.Token
{
	public interface ITokenHandler
	{
		DTOs.Token CreateAccessToken(int minute);
	}
}

