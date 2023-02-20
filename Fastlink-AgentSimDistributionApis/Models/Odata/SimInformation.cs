using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fastlink_AgentSimDistributionApis.Models.Odata
{
    public class SimInformation
    {
        public string UsrMDN { get; set; }
        public string UsrQrCode { get; set; }
        public string UsrPurchaseNumber { get; set; }
        public string UsrICCID { get; set; }
        public string UsrPIN1 { get; set; }
        public string UsrPIN2 { get; set; }
        public string UsrPUK1 { get; set; }
        public string UsrPUK2 { get; set; }
        public string UsrIMSI { get; set; }
        public string UsrSimTypeId { get; set; }
        public string UsrShowroomId { get; set; }
        public string UsrOwnerId { get; set; }
        public string UsrPrice { get; set; }
    }
}