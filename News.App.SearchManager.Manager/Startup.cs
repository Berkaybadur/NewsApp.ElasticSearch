using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using News.App.SearchManager.Infrastructure.Model.Elastic;
using News.App.SearchManager.Manager.Abstraction;
using News.App.SearchManager.Manager.IndexCreater;
using System;

namespace News.App.SearchManager.Manager
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
            services.AddScoped<INewsManager, NewsManager>();
            services.AddScoped<IElasticSearchManager, ElasticSearchManager>();
            services.AddScoped(typeof(IElasticIndexCreater<NewsContentElasticModel>), typeof(NewsIndexCreater));
        }
    }
}
