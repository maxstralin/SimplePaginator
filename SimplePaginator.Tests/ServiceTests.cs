using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimplePaginator.Tests
{
    public class ServiceTests
    {
        private IPaginationService PaginationService { get; } = new PaginationService();
        public IQueryable<int> Data { get; } = Enumerable.Range(1, 200).AsQueryable();

        [Theory]
        [InlineData(1, 50, 1, 50)]
        [InlineData(1, 25, 1, 25)]
        [InlineData(2, 25, 26, 50)]
        [InlineData(2, 50, 51, 100)]
        public void BasicPagination(int page, int pageSize, int start, int end)
        {
            var expected = Enumerable.Range(start, end - start + 1);

            var result = PaginationService.Paginate(Data, page, pageSize);

            Assert.Equal(page, result.Page);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(expected, result.Entries.AsEnumerable());
        }

        [Theory]
        [InlineData(25, 50, 2)]
        [InlineData(10, 50, 5)]
        [InlineData(12, 50, 5)]
        [InlineData(20, 50, 3)]
        public void PageCount(int pageSize, int totalEntries, int expectedPages)
        {
            var range = Enumerable.Range(1, totalEntries).AsQueryable();
            var result = PaginationService.Paginate(range, 1, pageSize);

            Assert.Equal(expectedPages, result.PageCount);
        }

        [Theory]
        [InlineData(200, 2)]
        [InlineData(90, 5)]
        public void CustomCountPageCount(int pageSize, int expectedPages)
        {
            var range = Enumerable.Range(1, 10).AsQueryable();

            var result = PaginationService.Paginate(range, 1, pageSize, (x) => 400);

            Assert.Equal(expectedPages, result.PageCount);
        }

        [Theory]
        [InlineData(200, 2)]
        [InlineData(90, 5)]
        public async Task CustomAsyncCountPageCount(int pageSize, int expectedPages)
        {
            var range = Enumerable.Range(1, 10).AsQueryable();

            var result = await PaginationService.PaginateAsync(range, 1, pageSize, (x) => Task.FromResult(400));

            Assert.Equal(expectedPages, result.PageCount);
        }

        [Fact]
        public void DependencyInjection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSimplePaginator();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var foundService = serviceProvider.GetRequiredService<IPaginationService>();

            Assert.NotNull(foundService);
            Assert.IsAssignableFrom<IPaginationService>(foundService);
            Assert.IsType<PaginationService>(foundService);
        }

    }
}
