using FluentAssertions;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Features.Queries.ProductQueries.GetAllProduct;
using CleanArchitecture.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Queries.ProductQueryTest
{
    public class GetAllProductTests:IClassFixture<BaseFixtures>
    {
        private readonly BaseFixtures testing;
        public GetAllProductTests(BaseFixtures testing)
        {
            this.testing = testing;
            this.testing.ResetState();
        }
        [Fact]
        public async Task ShouldReturnAllListsAndAssociatedItems()
        {
            // Arrange
            await testing.AddAsync(new Domain.Entities.Product
            {
                Name = "New Product"
            });

            var query = new GetAllProductQuery();

            // Act
            ServiceResult<List<ProductView>> result = await testing.SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().HaveCountGreaterThan(0);
        }


    }
}
