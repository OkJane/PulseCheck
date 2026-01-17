using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseCheck.Core.Enums;

namespace PulseCheck.Core.Models
{
    public class APIEndpoint
    {
        public long ID {  get; set; }
        public string URL { get; set; }
        public string RegisteredBy { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int FailureCount { get; set; }
        public int FailureThreshold { get; set; }
        public Status LastStatus { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
