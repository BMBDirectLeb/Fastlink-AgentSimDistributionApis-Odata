using Fastlink_AgentSimDistributionApis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Fastlink_AgentSimDistributionApis.Models.Odata;
using System.Web;

namespace Fastlink_AgentSimDistributionApis.Controllers
{
    [BasicAuthentication]
    public class AgentDistributionController : ApiController
    {
        List<Showroom> showroom = new List<Showroom>();
        List<Response> res = new List<Response>();
        [HttpPost]
        public HttpResponseMessage CreateSimInformation([FromBody]Showroom response)
        {
            if (!ModelState.IsValid)
            {
                List<Response> res = new List<Response>()
                {
                      new Response{code="400",Status="Bad Request", ErrorMessage="Body Error", Description="Please check the body something is missing"},
                };
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            else
            {
                try
                {

                    Odata odata = new Odata();
                    odata.OnLogRequest(response);
                    List<Response> res = new List<Response>()
                {
                     new Response{code="200",Status="OK", ErrorMessage="null", Description="Esim Records have been created in Creatio"},
                };
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (InvalidCastException e)
                {
                    List<Response> res = new List<Response>()
                {
                     new Response{code="400",Status="Bad Request", ErrorMessage=e.Source, Description="null"},
                };

                    if (e.Source != null)

                        Console.WriteLine("IOException source: {0}", e.Source);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }

        }

    }
}
