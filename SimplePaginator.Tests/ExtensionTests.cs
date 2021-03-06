using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SimplePaginator.Tests
{
    public class ExtensionTests
    {
        public IQueryable<int> Data { get; } = Enumerable.Range(1, 200).AsQueryable();

        [Fact]
        public void ExtensionMethodsImplementInterface()
        {
            var interfaceMethods = typeof(IPaginationService).GetMethods().ToList();
            
            var extensionMethods = typeof(PaginationExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();

            Assert.Equal(interfaceMethods.Count, extensionMethods.Count);
            Assert.All(interfaceMethods, iMethod =>
            {
                var exists = extensionMethods.Any(exMethod =>
                {
                    var exParams = exMethod.GetParameters().Select(a => a.GetType()).ToList();
                    var iParams = iMethod.GetParameters().Select(a => a.GetType()).ToList();

                    return exMethod.ReturnType.GetType() == iMethod.ReturnType.GetType() &&
                    exParams.Count == iParams.Count &&
                    exParams.SequenceEqual(iParams) &&
                    exMethod.GetGenericArguments().Count() == iMethod.GetGenericArguments().Count();
                });
                Assert.True(exists);
            });
        }

        [Theory]
        [InlineData(1, 50, 1, 50)]
        [InlineData(1, 25, 1, 25)]
        [InlineData(2, 25, 26, 50)]
        [InlineData(2, 50, 51, 100)]
        public void BasicPagination(int page, int pageSize, int start, int end)
        {
            var expected = Enumerable.Range(start, end-start+1);

            var result = Data.Paginate(page: 1, pageSize);

            Assert.Equal(page, result.Page);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(expected, result.Entries.AsEnumerable());
        }

        [Theory]
        [InlineData(25,50, 2)]
        [InlineData(10,50, 5)]
        [InlineData(12,50, 5)]
        [InlineData(20,50, 3)]
        public void PageCount(int pageSize, int totalEntries, int expectedPages)
        {
            var range = Enumerable.Range(1, totalEntries).AsQueryable();

            var result = range.Paginate(1, pageSize);

            Assert.Equal(expectedPages, result.PageCount);
        }

        [Theory]
        [InlineData(200, 2)]
        [InlineData(90, 5)]
        public void CustomCountPageCount(int pageSize, int expectedPages)
        {
            var range = Enumerable.Range(1, 10).AsQueryable();

            var result = range.Paginate(1, pageSize, (x) => 400);

            Assert.Equal(expectedPages, result.PageCount);
        }

        [Theory]
        [InlineData(200, 2)]
        [InlineData(90, 5)]
        public async Task CustomAsyncCountPageCount(int pageSize, int expectedPages)
        {
            var range = Enumerable.Range(1, 10).AsQueryable();

            var result = await range.PaginateAsync(1, pageSize, (x) => Task.FromResult(400));

            Assert.Equal(expectedPages, result.PageCount);
        }
    }
}