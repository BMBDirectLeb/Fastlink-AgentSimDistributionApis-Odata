using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fastlink_AgentSimDistributionApis.Models
{

    public class Showroom
    {
        public string ShowroomCode { get; set; }
        public int PurchaseNumber { get; set; }
        public List<Agent> Agents { get; set; }
        public string SimType { get; set; }
    }
}