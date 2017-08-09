using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Me20.ApiGateway.Identity;
using Me20.Contracts.Actors;
using Me20.IdentityActors;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Conventions;
using Ninject.Activation.Providers;
using Ninject;
using System;
using MassTransit.NinjectIntegration;

namespace Me20.ApiGateway
{
    public class MainBootstrapper : NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }

        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            // Perform registation that should have an application lifetime
            existingContainer.Bind<IBusControl>().ToMethod(ctx =>
                Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                {
                    var host = cfg.Host(
                        connectionString: Me20.Shared.BusConfig.AzureServiceBusConnectionString,
                        configure: hostCfg =>
                        {
                            hostCfg.OperationTimeout = TimeSpan.FromSeconds(5);
                        });

                    //cfg.ReceiveEndpoint(
                    //    host: host, 
                    //    queueName: "",
                    //    configure: ec =>
                    //    {
                    //        ec.LoadFrom(existingContainer);
                    //    });
                })).InSingletonScope();
            existingContainer.Bind<IBus, ISendEndpointProvider, IPublishEndpoint>().ToMethod(ctx => ctx.Kernel.Get<IBusControl>());

            existingContainer.Bind<ActorModel.ActorSystemContainer>().ToSelf().InSingletonScope();
            existingContainer.Bind<IKnowActor<UsersManagerActor>>().ToMethod(ctx => ctx.Kernel.Get<ActorModel.ActorSystemContainer>());
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
            container.Settings.AllowNullInjection = true;
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                var currentUser = new UserIdentity(System.Security.Claims.ClaimsPrincipal.Current);
                context.CurrentUser = currentUser;
                if (currentUser.IsValid)
                {
                    //Notify actor about login
                }

                return null;
            });
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Scripts/bundles"));
        }
    }

}