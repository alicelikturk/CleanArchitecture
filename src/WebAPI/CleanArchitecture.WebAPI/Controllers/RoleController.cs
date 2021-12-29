using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.AddClaimToRole;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.CreateRole;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.DeleteRole;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.RemoveClaimToRole;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.UpdateRole;
using CleanArchitecture.Application.Common.Features.Queries.RoleQueries.GetAllRole;
using CleanArchitecture.Application.Common.Features.Queries.RoleQueries.GetRoleById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Security;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllRoleQuery();
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetRoleByIdQuery() { Id = id };
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRoleCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateRoleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await mediator.Send(command));
        }


        [HttpPut("AddPermission")]
        public async Task<IActionResult> UpdatePermission(AddClaimToRoleCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("RemovePermission")]
        public async Task<IActionResult> UpdatePermission(RemoveClaimToRoleCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteRoleCommand() { Id = id }));
        }
    }
}
