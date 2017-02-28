using Me20.Core;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Me20.Web.Startup))]

namespace Me20.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();

            //Akka ActorModel
            ActorModel.StartActorSystem();
        }
    }
}