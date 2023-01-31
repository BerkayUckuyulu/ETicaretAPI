using System;
namespace ETİcaretAPI.Application.Abstraction.Services
{
	public interface IAuthService
	{
		Task<DTOs.Token> LoginAsync(string usernameOrEmail,string password, int accessTokenLifeTime);
	}
}

