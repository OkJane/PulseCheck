using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.Interfaces;
using PulseCheck.Core.Models;
using PulseCheck.Infrastructure.Data;

namespace PulseCheck.Infrastructure.Repositories
{
    public class HealthCheckLogRepository : IHealthCheckLogRepository
    {
        private PulseCheckDBContext _dbContext;
        public HealthCheckLogRepository(PulseCheckDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public async Task<HealthCheckLog> SaveHealthCheckLog(HealthCheckLog log)
        {
            try
            {
                await _dbContext.AddAsync(log);
                await _dbContext.SaveChangesAsync();
                return log;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n Exception: {ex.ToString} \n Inner Exception: {ex?.InnerException}");
            }
        }
    }
}
