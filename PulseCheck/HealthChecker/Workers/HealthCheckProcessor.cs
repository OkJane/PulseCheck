using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.Interfaces;
using PulseCheck.Core.Models;
using PulseCheck.Core.Models.Responses;
using PulseCheck.Infrastructure.ExternalClients;

namespace HealthChecker.Workers
{
    public class HealthCheckProcessor
    {
        private IHealthCheckLogRepository _logRepository;
        private HttpClients _httpClient;
        private IAPIEndpointRepository _apiEndpointRepository;
        public HealthCheckProcessor(IHealthCheckLogRepository logRepository, HttpClients httpClient, IAPIEndpointRepository apiEndpointRepository)
        {
            _logRepository = logRepository;
            _httpClient = httpClient;
            _apiEndpointRepository = apiEndpointRepository;
        }

        public async Task CheckHealth(APIEndpoint endpoint)
        {
            //Make request to endoint, get HealthCheckResult
            var healthCheckResult = await _httpClient.GET(endpoint.URL);

            //Log HealthCheckLog Response
            var response = new HealthCheckLog()
            {
                EndPointID = endpoint.ID,
                Status = healthCheckResult.isSuccess == true ? PulseCheck.Core.Enums.Status.Success : PulseCheck.Core.Enums.Status.Failure,
                ResponseTimeMs = healthCheckResult.ResponseTimeMs,
                DateCreated = DateTime.UtcNow,
            };

            //Log changes to APIEndpoint
            if(response.Status == PulseCheck.Core.Enums.Status.Failure )
            {
                endpoint.FailureCount += 1;
            }
            else
            {
                endpoint.FailureCount = 0;
            }
            //If FailureCount > set threashold, mark endpoint as down[
            endpoint.LastStatus = endpoint.FailureCount >= endpoint.FailureThreshold ? PulseCheck.Core.Enums.Status.Down : response.Status;
            endpoint.LastChecked = DateTime.UtcNow;

            //Save HealthCheckLog and Update APIEndpoint
            await _apiEndpointRepository.UpdateRegisteredEndpoint(endpoint);
            await _logRepository.SaveHealthCheckLog(response);

           
        }
    }
}
