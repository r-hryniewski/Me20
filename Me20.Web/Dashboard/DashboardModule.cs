using Nancy;

namespace Me20.Web.Dashboard
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
