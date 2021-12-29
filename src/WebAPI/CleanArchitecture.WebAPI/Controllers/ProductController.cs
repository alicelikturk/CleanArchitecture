using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Features.Commands.ProductCommands.CreateProduct;
using CleanArchitecture.Application.Common.Features.Commands.ProductCommands.DeleteProduct;
using CleanArchitecture.Application.Common.Features.Commands.ProductCommands.UpdateProduct;
using CleanArchitecture.Application.Common.Features.Queries.ProductQueries.GetAllProduct;
using CleanArchitecture.Application.Common.Features.Queries.ProductQueries.GetProductById;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public ProductController(IMediator mediator,ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet("CurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(currentUserService.UserId);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllProductQuery();
            return Ok(await mediator.Send(query));
        }

        [ResponseCache(Duration = 10)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdQuery() { Id = id };
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
