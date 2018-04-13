using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.ExceptionFilters
{
    public class TwitterCloneExceptionErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string actionname = filterContext.RouteData.Values["Action"].ToString();
            string controllername = filterContext.RouteData.Values["Controller"].ToString();
            Exception ex = filterContext.Exception;
            WriteError(ex, actionname, controllername);
            base.OnException(filterContext);
        }

        private void WriteError(Exception ex, string actioname, string controllername)
        {
            string content = @"Message: " + ex.Message + Environment.NewLine +
                "Action Name: " + actioname + Environment.NewLine +
                "Controller Name " + controllername + Environment.NewLine +
                "Exception Date: " + System.DateTime.Now + Environment.NewLine +"-------------------";            
            string path = @"C:\\Working\\ErrorLog.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(content);
            }
        }
    }
}