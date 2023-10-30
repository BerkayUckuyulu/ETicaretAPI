using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETİcaretAPI.Application.Features.Commands.AppUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUserCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginUserCommandRequest refreshTokenLoginUserCommandRequest)
        {
            RefreshTokenLoginUserCommandResponse refreshTokenLoginUserCommandResponse = await _mediator.Send(refreshTokenLoginUserCommandRequest);
            return Ok(refreshTokenLoginUserCommandResponse);
        }
    }
}

