using System;
using ETİcaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETİcaretAPI.Application.Features.Commands.AppUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.AppUser> _userManager;
        readonly SignInManager<Domain.AppUser> _signInManager;

        public LoginUserCommandHandler(UserManager<Domain.AppUser> userManager, SignInManager<Domain.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                ///// Yetkilerin belirlendiği kısım.
            }

            return new();
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
}

