using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using News.App.SearchManager.Data;
using News.App.SearchManager.Data.Abstraction;
using News.App.SearchManager.Infrastructure.Model.Elastic;
using News.App.SearchManager.Manager;
using News.App.SearchManager.Manager.Abstraction;
using News.App.SearchManager.Manager.ElasticSearchOptions.Configurations;
using News.App.SearchManager.Manager.IndexCreater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.App.SearchManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "News.App.SearchManager.API", Version = "v1" ,
                     Description= "Manage Elasticsearch",
                     Contact= new OpenApiContact
                     {
                         Name="NewsApp",
                         Email= "appdailynews@gmail.com"
                     }
                });
            });
            services.AddScoped<IElasticSearchConfigration, ElasticSearchConfigration>();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ElasticSearchManager>().As<IElasticSearchManager>();
            builder.RegisterType<NewsManager>().As<INewsManager>();
            builder.RegisterType<ElasticQueryCreater>().As<IElasticQueryCreater>();
            builder.RegisterType<ElasticClientCreater>().As<IElasticClientCreater>();
            builder.RegisterType<NewsIndexCreater>().As<IElasticIndexCreater<NewsContentElasticModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "News.App.SearchManager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
