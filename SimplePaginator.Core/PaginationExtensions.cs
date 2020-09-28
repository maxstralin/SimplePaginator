using Microsoft.Extensions.DependencyInjection;
using SimplePaginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaginator
{
    /// <summary>
    /// Extensions for paginating IQueryable<T>
    /// </summary>
    public static class PaginationExtensions
    {
        ///<inheritdoc cref="PaginationService.Paginate{T}(IQueryable{T}, int, int)"/>
        public static IPaginationResult<IQueryable<T>> Paginate<T>(this IQueryable<T> query, int page, int pageSize) => query.Paginate(page, pageSize, (x) => x.Count());

        ///<inheritdoc cref="Paginate{T}(IQueryable{T}, int, int, Func{IQueryable{T}, int})" />
        public static IPaginationResult<IQueryable<T>> Paginate<T>(this IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, int> countFunction)
        {
            var entriesCount = countFunction(query);
            return CreateResult(page, pageSize, query, entriesCount);
        }

        ///<inheritdoc cref="PaginateAsync{T}(IQueryable{T}, int, int, Func{IQueryable{T}, Task{int}})"/>
        public static async Task<IPaginationResult<IQueryable<T>>> PaginateAsync<T>(this IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, Task<int>> asyncCountFunction)
        {
            var entriesCount = await asyncCountFunction(query);
            return CreateResult(page, pageSize, query, entriesCount);
        }
        

        private static IPaginationResult<IQueryable<T>> CreateResult<T>(int page, int pageSize, IQueryable<T> query, int entriesCount)
        {
            return new PaginationResult<IQueryable<T>>(page, pageSize, query.Skip((page - 1)*pageSize).Take(pageSize), entriesCount);
        }

    }
}
