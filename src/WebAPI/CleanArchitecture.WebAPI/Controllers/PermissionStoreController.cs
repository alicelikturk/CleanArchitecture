using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Features.Commands.PermissionStoreCommands.CreatePermissionStore;
using CleanArchitecture.Application.Common.Features.Queries.PermissionStoreQueries.GetAllPermissionStore;
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
    public class PermissionStoreController : ControllerBase
    {
        private readonly IMediator mediator;

        public PermissionStoreController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllPermissionStoreQuery();
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePermissionStoreCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
