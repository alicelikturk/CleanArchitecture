using CleanArchitecture.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Extensions
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> CreatePaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        {
            
            return PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);
        }
    }
}
