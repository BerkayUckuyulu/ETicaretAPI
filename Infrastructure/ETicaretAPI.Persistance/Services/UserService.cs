using System;
using ETİcaretAPI.Application.Abstraction.Services;
using ETİcaretAPI.Application.DTOs;
using ETİcaretAPI.Application.Exceptions;
using ETİcaretAPI.Application.Features.Commands.AppUser;
using ETİcaretAPI.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
        {
            IdentityResult identityResult = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                NameSurname = createUserDto.NameSurname,

            }, createUserDto.Password);
            
            CreateUserResponseDto createUserReponseDto = new() { Succeeded = identityResult.Succeeded };

            if (identityResult.Succeeded)
                createUserReponseDto.Message = "Kullanıcı başarı ile oluşturuldu.";
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    createUserReponseDto.Message += $"{error.Code}--{error.Description}\n";
                }
            }
            return createUserReponseDto;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenEndDate, int addTimeOnAccesTokenDate)
        {
            if (appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenEndDate = accessTokenEndDate.AddMinutes(addTimeOnAccesTokenDate);

                await _userManager.UpdateAsync(appUser);
            }
            else
                throw new NotFoundUserExeption();

        }
    }
}

