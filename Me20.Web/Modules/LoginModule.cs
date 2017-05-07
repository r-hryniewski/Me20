using Me20.Web.ViewModels;
using Nancy;

namespace Me20.Web.Modules
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
