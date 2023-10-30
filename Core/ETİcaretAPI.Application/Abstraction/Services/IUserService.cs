using System;
using ETİcaretAPI.Application.DTOs;
using ETİcaretAPI.Domain;

namespace ETİcaretAPI.Application.Abstraction.Services
{
	public interface IUserService
	{
		Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
		Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenEndDate, int refreshTokenLifeTime);
	}
}

