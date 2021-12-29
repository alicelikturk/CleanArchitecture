using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Repositories
{
    public class PermissionStoreRepository:Repository<PermissionStore>,IPermissionStoreRepository
    {
        public PermissionStoreRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {

        }
    }
}
