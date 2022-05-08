using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryAssignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    internal class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            // Add new lines of code here to register any interfaces and concrete services you create
            services.AddTransient<IListItemService, ListItemService>();
            services.AddTransient<ISearchResultsService, SearchResultsService>();

            return services.BuildServiceProvider();
        }
    }
}
