using Identity.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Interface.Repository;
using UCP.Domain.Entity;

namespace UCP.Persistence.Repository
{
    public class RepositoryAsync : IRepository
    {
        private readonly UCPDbContext _dbContext;

        public RepositoryAsync(UCPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true, CancellationToken cancellationToken = default) where T : Member
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (AsNoTracking) query = query.AsNoTracking();
            return await query.Where(expression).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true, CancellationToken cancellationToken = default) where T : Member
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (AsNoTracking) query = query.AsNoTracking();
            if (expression != null) query = query.Where(expression);
            return await query.ToListAsync(cancellationToken);
        }





        //public async Task<PaginatedResult<TDto>> GetPaginatedListAsync<T, TDto>(PaginationFilter filter = null, Expression<Func<T, bool>> expression = null, bool AsNoTracking = true, CancellationToken cancellationToken = default) where T : BaseEntity
        //{
        //    IQueryable<T> query = _dbContext.Set<T>();
        //    query = (expression != null) ? query.Where(expression) : query.AsQueryable();
        //    return await query.ToMappedPaginatedResultAsync<T, TDto>(filter.PageNumber, filter.PageSize);
        //}


        public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        where T : Member
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (expression != null) return await query.AnyAsync(expression, cancellationToken);
            return await query.AnyAsync(cancellationToken);
        }

        public Task UpdateAsync<T>(T entity)
        where T : Member
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged)
            {
                throw new Exception("There is no update");
            }
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

          public Task UpdateAsyncForLoan<T>(T entity)
        where T : Loan
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged)
            {
                throw new Exception("There is no update");
            }
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }




        #region FirstOrDefault

        public async Task<T> FirstByConditionAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
             where T : Member
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (AsNoTracking)
                query = query.AsNoTracking();
            if (expression != null)
                query = query.Where(expression);
            return await query.FirstOrDefaultAsync();
        }

        #endregion FirstOrDefault

        #region LastOrDefault

        public async Task<T> LastByConditionAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
             where T : Member
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (AsNoTracking)
                query = query.AsNoTracking();
            if (expression != null)
                query = query.Where(expression);
            return await query.LastOrDefaultAsync();
        }

        #endregion LastOrDefault

        #region Create

        public async Task<Guid> CreateAsync<T>(T entity)
        where T : Loan
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }
           public async Task<Guid> ApplyAsync<T>(T entity)
        where T : ApplyForLoan
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }


        public async Task<IList<string>> CreateRangeAsync<T>(IEnumerable<T> entity)
        where T : Member
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
            return entity.Select(x => x.Id).ToList();
        }

        #endregion Create

        #region DeleteOrRemoveOrClear

        public Task RemoveAsync<T>(T entity)
        where T : Member
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
         public Task RemoveAsyncForloan<T>(T entity)
        where T : Loan
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }



        public async Task<T> RemoveByIdAsync<T>(Guid entityId)
        where T : Member
        {
            var entity = await _dbContext.Set<T>().FindAsync(entityId);
            if (entity == null) throw new DllNotFoundException("entity.notfound");
            _dbContext.Set<T>().Remove(entity);
            return entity;
        }

        //public async Task ClearAsync<T>(Expression<Func<T, bool>> expression = null)
        //where T : Member
        //{
        //    var query = _dbContext.Set<T>().AsQueryable();
        //    if (expression != null)
        //        query = query.Where(expression);

        //    await query.ForEachAsync(x =>
        //    {
        //        _dbContext.Entry(x).State = EntityState.Deleted;
        //    });
        //}

        #endregion DeleteOrRemoveOrClear

        #region Paginate



        #endregion Paginate

        #region Aggregations

        public async Task<int> CountByConditionAsync<T>(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
              where T : Member
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (expression != null)
                query = query.Where(expression);
            return await query.CountAsync();
        }

        #endregion Aggregations

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
