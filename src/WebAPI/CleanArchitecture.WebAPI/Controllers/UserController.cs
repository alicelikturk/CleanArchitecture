using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.CreateUser;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.DeleteUser;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUser;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUserRoles;
using CleanArchitecture.Application.Common.Features.Queries.UserQueries.GetAllUser;
using CleanArchitecture.Application.Common.Features.Queries.UserQueries.GetUserById;
using CleanArchitecture.Application.Common.Features.Queries.UserQueries.GetUsersInRole;
using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Security;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllUserQuery();
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetUserByIdQuery() { Id = id };
            return Ok(await mediator.Send(query));
        }

        [HttpGet("UsersInRole/{roleName}")]
        public async Task<IActionResult> Get(string roleName)
        {
            var query = new GetUsersInRoleQuery() { RoleName = roleName };
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await mediator.Send(command));
        }

        [HttpPut("UpdateRoles")]
        public async Task<IActionResult> Update(UpdateUserRolesCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteUserCommand() { Id = id }));
        }
    }
}
