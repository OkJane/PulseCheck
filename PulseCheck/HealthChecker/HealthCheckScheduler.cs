using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthChecker.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseCheck.Core.Interfaces;
using PulseCheck.Core.Models;

namespace HealthChecker
{
    public class HealthCheckScheduler : BackgroundService
    {
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(2);
        private readonly IServiceScopeFactory _serviceProvider;
        public HealthCheckScheduler(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                IEnumerable<APIEndpoint> registeredEndpoints;
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var _endpointRepository = scope.ServiceProvider.GetRequiredService<IAPIEndpointRepository>();
                    //Get all registered endpoints
                    registeredEndpoints = await _endpointRepository.GetRegisteredEndpoints();
                }
                    foreach (var registeredEndpoint in registeredEndpoints)
                    {
                        using (IServiceScope scope = _serviceProvider.CreateScope())
                        {
                            var healthCheckProcessor = scope.ServiceProvider.GetRequiredService<HealthCheckProcessor>();
                            await healthCheckProcessor.CheckHealth(registeredEndpoint);
                        }
                    }
                    
                
                await Task.Delay(_interval, cancellationToken);

            }
        }
    }
}
