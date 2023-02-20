using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fastlink_AgentSimDistributionApis.Models
{
    public class Availability
    {
      //  public string ESIM { get; set; }
        public string PRICE { get; set; }
        public string ICCID { get; set; }
        public string PIN1 { get; set; }
        public string PIN2 { get; set; }
        public string PUK1 { get; set; }
        public string PUK2 { get; set; }
        public string IMSI { get; set; }
        public string QrCode { get; set; }
        public string MDN { get; set; }
    }

}