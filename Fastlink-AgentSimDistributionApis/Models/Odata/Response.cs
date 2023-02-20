using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fastlink_AgentSimDistributionApis.Models.Odata
{
    public class Response
    {
        public string code { get; set; }
        public string Status { get; set; }
        public object ErrorMessage { get; set; }
        public string Description { get; set; }

    }
}