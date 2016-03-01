using System.Web;
using System.Web.Mvc;
using Ubikate.Core;

namespace Ubikate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TokenAuthorizationFilter());
        }
    }
}