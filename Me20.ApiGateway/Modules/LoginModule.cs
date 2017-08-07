using Me20.ApiGateway.ViewModels;
using Nancy;

namespace Me20.ApiGateway.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule() : base("/login")
        {
            Get["/"] = p =>
            {
                return View["login"];
            };
        }
    }
}
