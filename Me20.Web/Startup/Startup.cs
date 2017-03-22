using Me20.Core;
using Microsoft.Owin;
using Owin;
using System.Threading;

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

            //var context = new OwinContext(app.Properties);
            //var token = context.Get<CancellationToken>("host.OnAppDisposing");
            //if (token != CancellationToken.None)
            //{
            //    token.Register( async () =>
            //    {
            //        await ActorModel.MainActorSystem.Terminate();
            //    });
            //}
        }
    }
}