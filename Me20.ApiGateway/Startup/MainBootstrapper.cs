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
using Akka.DI.Ninject;
using Akka.Actor;

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
            #region MassTransit
            existingContainer.Bind<IBusControl>().ToMethod(ctx =>
                Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                {
                    var host = cfg.Host(
                        connectionString: Shared.BusConfig.AzureServiceBusConnectionString,
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
            #endregion

            #region Akka actor model
            var actorSystem = ActorSystem.Create(Me20.ActorModel.ActorPathsHelper.ActorSystemName);
            var propsResolver = new NinjectDependencyResolver(existingContainer, actorSystem);

            existingContainer.Bind<ActorModel.ActorSystemContainer>().ToSelf().InSingletonScope().WithConstructorArgument("actorSystem", actorSystem);
            existingContainer.Bind<IKnowActor<UsersManagerActor>>().ToMethod(ctx => ctx.Kernel.Get<ActorModel.ActorSystemContainer>());
            #endregion
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