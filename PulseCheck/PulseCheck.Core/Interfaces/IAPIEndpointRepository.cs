using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.DTO;
using PulseCheck.Core.Models;

namespace PulseCheck.Core.Interfaces
{
    public interface IAPIEndpointRepository
    {
        Task<APIEndpoint> RegisterEndpoint(RegisterAPIEndpointDTO endpoint);
        Task<List<APIEndpoint>> GetRegisteredEndpoints();
        Task<List<APIEndpoint>> GetRegisteredEndpointsByRegisterer(string registerer);
        Task<APIEndpoint> UpdateRegisteredEndpoint(APIEndpoint endpoint);
    }
}
