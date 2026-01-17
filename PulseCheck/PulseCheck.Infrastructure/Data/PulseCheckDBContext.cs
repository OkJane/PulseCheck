using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PulseCheck.Core.Models;

namespace PulseCheck.Infrastructure.Data
{
    public class PulseCheckDBContext : DbContext
    {
        public PulseCheckDBContext(DbContextOptions<PulseCheckDBContext> options) : base(options)
        {
            
        }
        public DbSet<APIEndpoint> APIEndpoints { get; set; }
        public DbSet<HealthCheckLog> HealthCheckLogs { get; set; }
    }
}
