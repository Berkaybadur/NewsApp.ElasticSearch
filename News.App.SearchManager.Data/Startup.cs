using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using News.App.SearchManager.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Data
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
            services.AddSingleton<IElasticClientCreater, ElasticClientCreater>();
            services.AddScoped<IElasticQueryCreater, ElasticQueryCreater>();
        }
    }
}
