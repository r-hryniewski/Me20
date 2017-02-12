using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me20.Web.Dashboard
{
    public class DashboardModule : NancyModule
    {
        public DashboardModule()
        {
            Get["/"] = x =>
            {
                return View["dashboard", new DashboardViewModel()];
            };
        }
    }
}