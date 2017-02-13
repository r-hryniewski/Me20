using Me20.Web.Identity;
using Nancy;
using Nancy.Bootstrapper;

namespace Me20.Web
{
    public class UserIdentityProviderBootstrapper : IRequestStartup
    {
        public void Initialize(IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                context.CurrentUser = new User(System.Security.Claims.ClaimsPrincipal.Current.Identity);
                return null;
            });

        }
    }
}