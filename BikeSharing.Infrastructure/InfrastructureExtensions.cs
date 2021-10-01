using BikeSharing.Domain.Repositories;
using BikeSharing.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();

            return services;
        }
    }
}
