using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.Models.Responses;

namespace PulseCheck.Infrastructure.ExternalClients
{
    public class HttpClients
    {
        private readonly HttpClient _httpClient;
        public HttpClients(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }
        public async Task<HealthCheckResult> GET(string url)
        {
            Stopwatch latency = Stopwatch.StartNew();
            try
            {
                var response = await _httpClient.GetAsync(url);
                latency.Stop();
                return new HealthCheckResult
                {
                    isSuccess = response.IsSuccessStatusCode,
                    ResponseTimeMs = latency.ElapsedMilliseconds,
                };
            }
            catch (Exception ex)
            {
                latency.Stop();
                return new HealthCheckResult
                {
                    isSuccess = false,
                    ResponseTimeMs = latency.ElapsedMilliseconds,
                };
            }
        }
    }
}
