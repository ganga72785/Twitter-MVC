using System.Web;
using System.Web.Mvc;
using WebApplication1.ExceptionFilters;

namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TwitterCloneExceptionErrorAttribute());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}
