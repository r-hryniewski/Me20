using Me20.ApiGateway.ViewModels;
using Nancy;

namespace Me20.ApiGateway.Modules
{
    public class DashboardModule : NancyModule
    {
        public DashboardModule()
        {
            Before.AddItemToEndOfPipeline(
                ctx =>
                {
                    if (ctx.CurrentUser == null || string.IsNullOrWhiteSpace(ctx.CurrentUser.UserName))
                        return Response.AsRedirect("/login");

                    return null;
                });

            Get["/"] = p =>
            {
                return View["dashboard", new DashboardViewModel(Context.CurrentUser)];
            };
        }
    }
}
