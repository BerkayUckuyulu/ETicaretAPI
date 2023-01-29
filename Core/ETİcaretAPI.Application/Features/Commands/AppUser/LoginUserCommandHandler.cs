using System;
using ETİcaretAPI.Application.Abstraction.Token;
using ETİcaretAPI.Application.DTOs;
using ETİcaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETİcaretAPI.Application.Features.Commands.AppUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.AppUser> _userManager;
        readonly SignInManager<Domain.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<Domain.AppUser> userManager, SignInManager<Domain.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           Domain.AppUser appUser= await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (appUser == null)
                appUser=await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser == null)
                throw new NotFoundUserExeption();

           SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);

            if (signInResult.Succeeded)
            {
               Token token =_tokenHandler.CreateAccessToken(5);
                return new LoginUserCommandSuccessResponse() { Token = token };

            }

            throw new AuthenticationErrorException();
            //return new LoginUserCommandErrorResponse() { Message="Kullanıcı adı veya şifre hatalıdır."};
        }
    }

    public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandResponse
    {

    }
    public class LoginUserCommandSuccessResponse:LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserCommandErrorResponse:LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}

