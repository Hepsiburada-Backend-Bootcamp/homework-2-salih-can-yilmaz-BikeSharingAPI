using BikeSharing.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Application
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddInfrastructureModule();

            return services;
        }
    }
}
