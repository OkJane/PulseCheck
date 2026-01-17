using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.Enums;

namespace PulseCheck.Core.Models
{
    public class HealthCheckLog
    {
        public long Id { get; set; }
        public long EndPointID { get; set; }
        public Status Status { get; set; }
        public decimal ResponseTimeMs { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
