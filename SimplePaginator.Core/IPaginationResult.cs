namespace SimplePaginator
{
    public interface IPaginationResult<T>
    {
        int Page { get; }
        int PageCount { get; }
        int PageSize { get; }
        int EntriesCount { get; }
        T Entries { get; }
    }
}