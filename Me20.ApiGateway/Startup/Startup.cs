using Me20.ActorModel;
using Microsoft.Owin;
using Serilog;
using System.Configuration;
using Owin;

[assembly: OwinStartup(typeof(Me20.ApiGateway.Startup))]
namespace Me20.ApiGateway
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .ApplicationInsightsEvents(ConfigurationManager.AppSettings["AppliactionInsigthsInstrumentationKey"])
                .CreateLogger();

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