using System;
using ETİcaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETİcaretAPI.Application.Features.Commands.AppUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<Domain.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult identityResult = await _userManager.CreateAsync(new()
            {
                Id=Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname,

            }, request.Password);

            CreateUserCommandResponse createUserCommandResponse = new() { Succeeded = identityResult.Succeeded };

            if (identityResult.Succeeded)
                createUserCommandResponse.Message = "Kullanıcı başarı ile oluşturuldu.";
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    createUserCommandResponse.Message += $"{error.Code}--{error.Description}\n";
                }
            }
            return createUserCommandResponse;

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

