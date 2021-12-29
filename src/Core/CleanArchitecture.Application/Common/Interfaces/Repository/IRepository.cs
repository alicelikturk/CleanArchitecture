using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public IQueryable<T> Table{ get;}
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate,string sort,string direction,int pageNumber, int pageSize, CancellationToken token = default);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<bool> AllByConditionAsync(Expression<Func<T, bool>> predicateSelect, Expression<Func<T, bool>> predicateCondition, CancellationToken token = default);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(List<T> entities);
        Task DeleteAsync(T entity, bool isCascade = false);
        Task DeleteAsync(List<T> entities);
    }
}
