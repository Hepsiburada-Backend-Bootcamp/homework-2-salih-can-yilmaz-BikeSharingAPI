using BikeSharing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing.Infrastructure.Context
{
    public class BikeSharingDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public BikeSharingDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
