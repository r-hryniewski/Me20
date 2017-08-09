using Me20.ApiGateway.ViewModels;
using Nancy;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using System;
using Me20.Contracts;

namespace Me20.ApiGateway.Modules
{
    public class DashboardModule : NancyModule
    {
        private ISendEndpointProvider endpointProvider;

        public DashboardModule(ISendEndpointProvider endpointProvider)
        {
            this.endpointProvider = endpointProvider;

            Before.AddItemToEndOfPipeline(
                ctx =>
                {
                    if (ctx.CurrentUser == null || string.IsNullOrWhiteSpace(ctx.CurrentUser.UserName))
                        return Response.AsRedirect("/login");
            
                    return null;
                });

            Get["/", true] = async (p, ct) =>
            {

                //var sendEnpoint = await endpointProvider.GetSendEndpoint(Shared.BusConfig.ContentReadQueueUri);
                //await sendEnpoint.Send<something>(new { });
                return View["dashboard", new DashboardViewModel(Context.CurrentUser)];
            };
        }
    }
}
