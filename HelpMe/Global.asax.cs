using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HelpMe.BusinessAccess;
using HelpMe.Helpers;

namespace HelpMe
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BeanHelper.DBHelper.ProviderName = ConfigurationManager.AppSettings["SQLProvider"].ToString();
            BeanHelper.DBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
            LogManager.Configure(Server.MapPath("~/log4net.config"));
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}