using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Me20.Web.Startup))]

namespace Me20.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseNancy();
            //TODO: Create ActorSystem and stuff
        }
    }
}