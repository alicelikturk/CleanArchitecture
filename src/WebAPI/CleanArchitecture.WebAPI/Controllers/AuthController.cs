using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Features.Commands.AuthenticateCommands.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Signin")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        //[AllowAnonymous]
        //[HttpPost("Signup")]
        //public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
        //{
        //    return Ok(await mediator.Send(command));
        //}
    }
}
