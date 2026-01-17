using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.Models;

namespace PulseCheck.Core.Interfaces
{
    public interface IHealthCheckLogRepository
    {
        Task<HealthCheckLog> SaveHealthCheckLog(HealthCheckLog log);
    }
}
