using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CleanArchitecture.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this.dbContext = applicationDbContext;
        }

        public IQueryable<T> Table { get => dbContext.Set<T>(); }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            return await dbContext.Set<T>().AllAsync(predicate, token);
        }

        public async Task<bool> AllByConditionAsync(Expression<Func<T, bool>> predicateSelect, Expression<Func<T, bool>> predicateCondition, CancellationToken token = default)
        {
            return await dbContext.Set<T>()
                .Where(predicateSelect)
                .AllAsync(predicateCondition, token);
        }

        public async Task DeleteAsync(T entity, bool isCascade = false)
        {
            if (isCascade)
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                await dbContext.SaveChangesAsync();
            }
            else
            {
                if (entity.GetType().GetProperty("IsDeleted") != null)
                {
                    T deletedEntity = entity;
                    deletedEntity.GetType().GetProperty("IsDeleted")?.SetValue(deletedEntity, true);
                    await UpdateAsync(deletedEntity);
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteAsync(List<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            return await dbContext.Set<T>().Where(predicate).ToListAsync(token);
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate, string sort, string direction, int pageNumber, int pageSize, CancellationToken token = default)
        {
            return dbContext.Set<T>().Where(predicate)
                .OrderBy(sort + " " + direction)
                .Skip(pageNumber)
                .Take(pageSize);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
