using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PulseCheck.Core.DTO;
using PulseCheck.Core.Interfaces;
using PulseCheck.Core.Models;
using PulseCheck.Infrastructure.Data;

namespace PulseCheck.Infrastructure.Repositories
{
    public class APIEndpointRepository : IAPIEndpointRepository
    {
        private PulseCheckDBContext _dbContext;
        public APIEndpointRepository(PulseCheckDBContext dBContext)
        {
            this._dbContext = dBContext;
        }

        public async Task<APIEndpoint> RegisterEndpoint(RegisterAPIEndpointDTO endpointDTO)
        {
            try
            {
                APIEndpoint endpoint = new APIEndpoint
                {
                    URL = endpointDTO.URL,
                    RegisteredBy = endpointDTO.RegisteredBy,
                    RegisteredAt = DateTime.UtcNow,
                    FailureCount = 0,
                    FailureThreshold = endpointDTO.FailureThreshold,
                    
                };
                await _dbContext.AddAsync(endpoint);
                await _dbContext.SaveChangesAsync();
                return endpoint;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n Exception: {ex.ToString} \n Inner Exception: {ex?.InnerException}");
            }
        }

        public async Task<List<APIEndpoint>> GetRegisteredEndpoints()
        {
            try
            {
                var result = await _dbContext.APIEndpoints.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n Exception: {ex.ToString} \n Inner Exception: {ex?.InnerException}");
            }
        }

        public async Task<List<APIEndpoint>> GetRegisteredEndpointsByRegisterer(string registerer)
        {
            try
            {
                var result = await _dbContext.APIEndpoints.Where(x => x.RegisteredBy.ToLower() == registerer.ToLower()).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message} \n Exception: {ex.ToString} \n Inner Exception: {ex?.InnerException}");
            }
        }

        public async Task<APIEndpoint> UpdateRegisteredEndpoint(APIEndpoint endpoint)
        {
            try
            {
                var existingEndpoint = await _dbContext.APIEndpoints.AsNoTracking().FirstOrDefaultAsync(x => x.ID == endpoint.ID);
                if(existingEndpoint != null)
                {
                    _dbContext.APIEndpoints.Update(endpoint);
                    await _dbContext.SaveChangesAsync();
                    return endpoint;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n Exception: {ex.ToString()} \n Inner Exception: {ex?.InnerException}");
            }
        }
    }
}
