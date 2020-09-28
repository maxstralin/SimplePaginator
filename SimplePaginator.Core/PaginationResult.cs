using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePaginator
{
    public class PaginationResult<T> : IPaginationResult<T>
    {
        public PaginationResult(int page, int pageSize, T result, int entriesCount)
        {
            Page = page;
            PageSize = pageSize;
            Entries = result;
            PageCount = (int)Math.Ceiling(entriesCount / (double)pageSize);
            EntriesCount = entriesCount;
        }

        public int Page { get; }
        public int PageSize { get; }
        public T Entries { get; }
        public int PageCount { get; }

        public int EntriesCount { get; }
    }


}
