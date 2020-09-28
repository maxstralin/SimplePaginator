using SimplePaginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaginator
{
    public class PaginationService : IPaginationService
    {
        ///<inheritdoc/>
        public IPaginationResult<IQueryable<T>> Paginate<T>(IQueryable<T> query, int page, int pageSize) => query.Paginate(page, pageSize);

        ///<inheritdoc/>
        public IPaginationResult<IQueryable<T>> Paginate<T>(IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, int> countFunction) => query.Paginate(page, pageSize, countFunction);

        ///<inheritdoc/>
        public Task<IPaginationResult<IQueryable<T>>> PaginateAsync<T>(IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, Task<int>> asyncCountFunction) => query.PaginateAsync(page, pageSize, asyncCountFunction);
    }
}
