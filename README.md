# SimplePagination

A very simple pagination library that takes an `IQueryable<T>` and returns a paginated result. Can be used both as extension methods or through DI.

For MongoDb, install [SimplePagination.MongoDb](https://github.com/maxstralin/SimplePaginator.MongoDb), which enables better pagination for `IMongoQueryable<T>`.

## Installation

Use Visual Studio Package Manager or 
```bash
Install-Package SimplePaginator
```

## Usage
Have a look at the tests for more examples.

### Extension methods
```csharp
var source = Enumerable.Range(1,100).AsQueryable();

//Returns an IPaginationResult with the first 15 entries, where the page count is calculated using LINQ's count() function 
IPaginationResult paginated = source.Paginate(page: 1, pageSize: 15);
//paginated.Page == 1
//paginated.PageCount == 7
//paginated.PageSize == 15
//paginated.EntriesCount == 100
```

Use a custom count function for returning number of entries. The idea is to use this when LINQ's default `Count()` function isn't what you're looking for.
```csharp
var source = Enumerable.Range(1,100).AsQueryable();

//Returns an IPaginationResult with the first 15 entries, where the page count is calculated using a custom function.
IPaginationResult paginated = source.Paginate(page: 1, pageSize: 15, (q) => 50);
//paginated.Page == 1
//paginated.PageCount == 4
//paginated.PageSize == 15
//paginated.EntriesCount == 50
```

You can also use an async custom function
```csharp
//Returns an IPaginationResult with the first 15 entries, where the page count is calculated using a custom async function.
IPaginationResult paginated = await source.PaginateAsync(page: 1, pageSize: 15, (q) => Task.FromResult(50));
//paginated.Page == 1
//paginated.PageCount == 4
//paginated.PageSize == 15
//paginated.EntriesCount == 50
```

### Dependency injection
Behind the scenes, the default service simply calls the extensions methods so there's no practical difference.  
**Startup.cs**
```csharp
services.AddSimplePaginator(); 
```

```csharp
//Injected through DI
public void Example(IPaginationService pagination) {
 var source = Enumerable.Range(1,100).AsQueryable();

 //Returns an IPaginationResult with the first 15 entries, where the page count is calculated using LINQ's count() function 
 IPaginationResult paginated = pagination.Paginate(query: source, page: 1, pageSize: 15);
 //paginated.Page == 1
 //paginated.PageCount == 7
 //paginated.PageSize == 15
 //paginated.EntriesCount == 100
}
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)