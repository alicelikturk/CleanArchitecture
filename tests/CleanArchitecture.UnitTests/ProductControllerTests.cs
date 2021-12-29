using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Features.Queries.ProductQueries.GetProductById;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.WebAPI.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetById_With()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var currentUserService= new Mock<ICurrentUserService>();

            var expectedItem = createNullResult();

            mediator.Setup(x => x.Send(It.IsAny<GetProductByIdQuery>(),CancellationToken.None))
                .Returns(Task.Run(()=> expectedItem));

            var controller = new ProductController(mediator.Object, currentUserService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetById(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            ((ServiceResult<ProductView>)result.Value).Succeeded.Should().BeTrue();
            ((ServiceResult<ProductView>)result.Value).Should().BeEquivalentTo(expectedItem);
        }

        ServiceResult<ProductView> createNullResult()
        {
            return new ServiceResult<ProductView>(true,new string[] { },new ProductView())
            {
                
            };
        }
    }
}
