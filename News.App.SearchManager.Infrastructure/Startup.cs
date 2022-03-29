using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure
{
    public class Startup
    {
        public int Order => 0;

        public void Configure(IApplicationBuilder application)
        {
            // Method intentionally left empty.
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Method intentionally left empty.
        }
    }
}
