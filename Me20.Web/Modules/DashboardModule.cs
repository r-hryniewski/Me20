﻿using Me20.Web.ViewModels;
using Nancy;

namespace Me20.Web.Modules
{
    public class DashboardModule : NancyModule
    {
        public DashboardModule()
        {
            Get["/"] = p =>
            {
                return View["dashboard", new DashboardViewModel(Context.CurrentUser)];
            };
        }
    }
}