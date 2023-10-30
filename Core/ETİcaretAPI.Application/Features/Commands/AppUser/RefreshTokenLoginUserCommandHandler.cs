using System;
using ETİcaretAPI.Application.Abstraction.Services;
using ETİcaretAPI.Application.DTOs;
using MediatR;

namespace ETİcaretAPI.Application.Features.Commands.AppUser
{
    public class RefreshTokenLoginUserCommandHandler : IRequestHandler<RefreshTokenLoginUserCommandRequest, RefreshTokenLoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginUserCommandResponse> Handle(RefreshTokenLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new() { Token = token };
        }

    }
    public class RefreshTokenLoginUserCommandRequest : IRequest<RefreshTokenLoginUserCommandResponse>
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenLoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
}




