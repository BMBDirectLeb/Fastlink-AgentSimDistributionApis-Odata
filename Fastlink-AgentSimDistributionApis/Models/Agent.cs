using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fastlink_AgentSimDistributionApis.Models
{
    public class Agent
    {
        public string Id { get; set; }
        public List<Availability> Availability { get; set; }
    }
}