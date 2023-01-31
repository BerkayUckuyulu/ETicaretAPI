using System;
using ETİcaretAPI.Application.Abstraction.Services;
using ETİcaretAPI.Application.Abstraction.Token;
using ETİcaretAPI.Application.DTOs;
using ETİcaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETİcaretAPI.Application.Features.Commands.AppUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           Token token= await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 5);
            return new LoginUserCommandSuccessResponse()
            {
                Token = token
            };
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

