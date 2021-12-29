using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Persistence.Repositories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {

        }
    }
}
