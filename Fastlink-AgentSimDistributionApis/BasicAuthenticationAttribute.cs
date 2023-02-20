using Fastlink_AgentSimDistributionApis.Models.Odata;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Fastlink_AgentSimDistributionApis
{                    
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
       
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                 
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                  
                var arrUserNameandPassword = decodeauthToken.Split(':');

                if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                           new GenericIdentity(arrUserNameandPassword[0]), null);
                }
                else
                {
                    List<Response> res = new List<Response>()
                    {
                         new Response{code = "401",Status ="Unauthorized" , ErrorMessage="Invalid Username or Password", Description="null"},
                    };
                    actionContext.Response = actionContext.Request
                        .CreateResponse(res);// HttpStatusCode.Unauthorized+", Invalid Username or password");

                }
            }
            else
            {
                List<Response> res = new List<Response>()
                    {
                         new Response{code = "401",Status ="Unauthorized" ,ErrorMessage="Invalid Username or Password", Description="null"},
                    };
                actionContext.Response = actionContext.Request
                    .CreateResponse(res);
            }
        }
        public static bool IsAuthorizedUser(string Username, string Password)
        {
            string User = ConfigurationManager.AppSettings["Username"];
            string Pass = ConfigurationManager.AppSettings["Password"];

            return Username == User && Password == Pass;
        }
    }
}