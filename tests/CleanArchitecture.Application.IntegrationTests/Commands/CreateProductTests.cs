using FluentAssertions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Features.Commands.ProductCommands.CreateProduct;
using CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Commands
{
    public class CreateProductTests : IClassFixture<BaseFixtures>
    {
        private readonly BaseFixtures testing;
        public CreateProductTests(BaseFixtures testing)
        {
            this.testing = testing;
            this.testing.ResetState();
        }

        [Fact]
        public void ShouldRequireMinumunFields()
        {
            var command = new CreateProductCommand();

            FluentActions.Invoking(() => testing.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldRequireUniqueName()
        {
            await testing.SendAsync(new CreateProductCommand
            {
                Name = "New Product"
            });

            var command = new CreateProductCommand
            {
                Name = "New Product"
            };

            await FluentActions.Invoking(() =>
            testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
