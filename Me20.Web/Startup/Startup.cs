﻿using Me20.Core;
using Microsoft.Owin;
using Owin;
using Serilog;
using System.Configuration;

[assembly: OwinStartup(typeof(Me20.Web.Startup))]

namespace Me20.Web
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