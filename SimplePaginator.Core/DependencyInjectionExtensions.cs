using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePaginator
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds the default implementation of <seealso cref="IPaginationService"/>
        /// </summary>
        public static void AddSimplePaginator(this IServiceCollection services)
        {
            services.AddTransient<IPaginationService, PaginationService>();
        }
    }
}
