using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseCheck.Core.Models.Responses
{
    public class HealthCheckResult
    {
        public bool isSuccess {  get; set; }
        public long ResponseTimeMs { get; set; }
    }
}
