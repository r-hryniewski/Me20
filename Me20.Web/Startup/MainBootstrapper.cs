using Me20.Common.Interfaces;
using Me20.Core.Contents;
using Me20.Core.Identity;
using Me20.Core.Tags;
using Me20.Web.Identity;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Conventions;
using Ninject;

namespace Me20.Web
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
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
            container.Settings.AllowNullInjection = true;

            //Not used at the moment
            //container.Bind<IDispatch<Tag>>().To<CreateTagIfNotExistsDispatcher>();
            container.Bind<IDispatch<TagEntity>>().To<TagSubscribedDispatcher>();

            container.Bind<IDispatch<UserEntity>>().To<UserLoggedInDispatcher>();

            //container.Bind<IDispatch<Content>>().To<CreateContentIfNotExistsDispatcher>();
            container.Bind<IDispatch<ContentEntity>>().To<AddContentDispatcher>();
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                var currentUser = new UserEntity(System.Security.Claims.ClaimsPrincipal.Current)
                .With(container.GetAll<IDispatch<UserEntity>>());

                if (currentUser.IsValid)
                {
                    //TODO: Store User in session or cache
                    currentUser.DispatchAll(currentUser.UserName);
                    context.CurrentUser = new UserIdentity(currentUser);
                }
                else
                    context.CurrentUser = UserIdentity.Empty;

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