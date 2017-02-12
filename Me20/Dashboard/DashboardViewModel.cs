using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me20.Web.Dashboard
{
    public class DashboardViewModel
    {
        public string UserGuid { get; private set; }

        public DashboardViewModel()
        {
            UserGuid = Guid.NewGuid().ToString();
        }
    }
}