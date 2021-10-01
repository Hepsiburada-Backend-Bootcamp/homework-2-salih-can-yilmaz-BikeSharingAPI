using BikeSharing.Domain.Repositories;
using BikeSharing.Infrastructure.Context;
using BikeSharing.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BikeSharingDBContext>(
                options => options.UseSqlite($"Data Source={configuration.GetConnectionString("SQLite")}",
                x => x.MigrationsAssembly("BikeSharingAPI")));

            services.AddScoped<BikeSharingDBContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();

            return services;
        }
    }
}
