using System;
using ETİcaretAPI.Application.DTOs;

namespace ETİcaretAPI.Application.Abstraction.Services
{
	public interface IUserService
	{
		Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
	}
}

