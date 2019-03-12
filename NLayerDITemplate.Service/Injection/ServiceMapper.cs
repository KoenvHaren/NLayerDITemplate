using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerDITemplate.Data.Abstract;
using NLayerDITemplate.Data.Concrete;
using NLayerDITemplate.Service.Abstract;
using NLayerDITemplate.Service.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerDITemplate.Service.Injection
{
    public class ServiceMapper
    {
        /// <summary>
        /// This method mirrors the ConfigureServices dependancy injection seen in Startup.cs in the AppLogic layer, 
        /// to maintain the project references structure
        /// For a list on Service lifetimes, Scoped vs Transient etc. please see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes
        /// </summary>
        /// <param name="services">Service map for DI</param>
        /// <param name="configuration">Config files wherein connection strings lie</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            //services.AddDbContext

            //Services
            services.AddTransient<IValueService, ValueService>();

            //Repositories
            services.AddScoped<IValueRepository, ValueRepository>();
        }
    }
}
