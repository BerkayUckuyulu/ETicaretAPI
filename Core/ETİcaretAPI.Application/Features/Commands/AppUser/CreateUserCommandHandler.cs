using System;
using ETİcaretAPI.Application.Abstraction.Services;
using ETİcaretAPI.Application.DTOs;
using ETİcaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETİcaretAPI.Application.Features.Commands.AppUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

           CreateUserResponseDto createUserResponseDto= await _userService.CreateAsync(new() { Email = request.Email, NameSurname = request.NameSurname, Password = request.Password, PasswordConfirm = request.PasswordConfirm, UserName = request.UserName });

            return new CreateUserCommandResponse()
            {
                Message = createUserResponseDto.Message,
                Succeeded = createUserResponseDto.Succeeded
            };
        }
    }
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

    }
    public class CreateUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}

