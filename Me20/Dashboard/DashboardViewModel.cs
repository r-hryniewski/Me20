using System;

namespace Me20.Web.Dashboard
{
    public class DashboardViewModel
    {
        public string UserGuid { get; private set; }
        public string UserIdentity { get; set; }

        public DashboardViewModel()
        {
            UserGuid = Guid.NewGuid().ToString();
        }
    }
}