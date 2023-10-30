using System;
using ETİcaretAPI.Application.Abstraction.Services;
using ETİcaretAPI.Application.Abstraction.Token;
using ETİcaretAPI.Application.DTOs;
using ETİcaretAPI.Application.Exceptions;
using ETİcaretAPI.Application.Features.Commands.AppUser;
using ETİcaretAPI.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IUserService _userService;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            ETİcaretAPI.Domain.AppUser appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (appUser == null)
                throw new NotFoundUserExeption();

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);

            if (signInResult.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,appUser);
                await _userService.UpdateRefreshToken(token.RefreshToken, appUser, token.Expiration, 5);
                return token;

            }

            throw new AuthenticationErrorException();
            //return new LoginUserCommandErrorResponse() { Message="Kullanıcı adı veya şifre hatalıdır."};
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(5,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);
                return token;

            }
            else
                throw new NotFoundUserExeption();
        }
    }
}

