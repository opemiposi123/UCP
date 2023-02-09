using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Repository
{
    public interface IRepository
    {
        #region Get
        Task<T> GetAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true, CancellationToken cancellationToken = default) where T : Member;

        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> expression = null, bool AsNoTracking = true, CancellationToken cancellationToken = default) where T : Member;

        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        where T : Member;

        #endregion

        #region update
        Task UpdateAsync<T>(T entity)
        where T : Member;

         Task UpdateAsyncForLoan<T>(T entity)
        where T : Loan;


        #endregion update

        #region Save Changes

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Save Changes

        #region FirstOrDefault

        Task<T> FirstByConditionAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
        where T : Member;

        #endregion FirstOrDefault

        #region LastOrDefault

        Task<T> LastByConditionAsync<T>(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
        where T : Member;

        #endregion LastOrDefault

        #region Create

        Task<Guid> CreateAsync<T>(T entity)
        where T : Loan;
          Task<Guid> ApplyAsync<T>(T entity)
        where T : ApplyForLoan; 

        Task<IList<string>> CreateRangeAsync<T>(IEnumerable<T> entity)
        where T : Member;

        #endregion Create

        #region DeleteOrRemoveOrClear

        Task RemoveAsync<T>(T entity)
        where T : Member;

         Task RemoveAsyncForloan<T>(T entity)
        where T : Loan;

        Task<T> RemoveByIdAsync<T>(Guid entityId)
        where T : Member;

        //Task ClearAsync<T>(Expression<Func<T, bool>> expression = null)
        //where T : BaseEntity;

        #endregion Paginate
        //Task<PaginatedResult<TDto>> GetPaginatedListAsync<T, TDto>(PaginationFilter filter = null, Expression<Func<T, bool>> expression = null, bool AsNoTracking = true, CancellationToken cancellationToken = default) where T : BaseEntity;

        #region Aggregations
        Task<int> CountByConditionAsync<T>(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
        where T : Member;
        #endregion Aggregations
    }

}
