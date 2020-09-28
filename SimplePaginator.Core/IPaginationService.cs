using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePaginator
{
    public interface IPaginationService
    {
        /// <summary>
        /// Paginate an IQueryable, getting the entries count using Linq
        /// </summary>
        /// <typeparam name="T">Entry type</typeparam>
        /// <param name="query">The query to paginate</param>
        /// <param name="page">Page number, starting at 1</param>
        /// <param name="pageSize">Number of entries per page</param>
        /// <returns>Paginated query with page numbers calculated</returns>
        IPaginationResult<IQueryable<T>> Paginate<T>(IQueryable<T> query, int page, int pageSize);
        /// <summary>
        /// Paginate an IQueryable with a page count returned using a custom count function
        /// </summary>
        /// <typeparam name="T">Entry type</typeparam>
        /// <param name="query">The query to paginate</param>
        /// <param name="page">Page number, starting at 1</param>
        /// <param name="pageSize">Number of entries per page</param>
        /// <param name="countFunction">Custom count function</param>
        /// <returns>Paginated query with page numbers calculated</returns>
        IPaginationResult<IQueryable<T>> Paginate<T>(IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, int> countFunction);
        /// <summary>
        /// Paginate an IQueryable with a page count returned using a custom async count function
        /// </summary>
        /// <typeparam name="T">Entry type</typeparam>
        /// <param name="query">The query to paginate</param>
        /// <param name="page">Page number, starting at 1</param>
        /// <param name="pageSize">Number of entries per page</param>
        /// <param name="asyncCountFunction">Custom async count function</param>
        /// <returns>Paginated query with page numbers calculated</returns>
        Task<IPaginationResult<IQueryable<T>>> PaginateAsync<T>(IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, Task<int>> asyncCountFunction);
    }
}