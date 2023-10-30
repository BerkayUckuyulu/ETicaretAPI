using System;
using ETİcaretAPI.Domain;

namespace ETİcaretAPI.Application.Abstraction.Token
{
	public interface ITokenHandler
	{
		DTOs.Token CreateAccessToken(int minute,AppUser appUser);
		string CreateRefreshToken();
	}
}

