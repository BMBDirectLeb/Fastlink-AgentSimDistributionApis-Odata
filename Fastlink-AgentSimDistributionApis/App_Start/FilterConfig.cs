using System.Web;
using System.Web.Mvc;

namespace Fastlink_AgentSimDistributionApis
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
