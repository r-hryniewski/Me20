using Me20.Web.Identity;
using Nancy;
using Nancy.Bootstrapper;
using UserDTO = Me20.Core.DTO.UserDTO;

namespace Me20.Web
{
    public class UserIdentityProviderBootstrapper : IRequestStartup
    {
        public void Initialize(IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                var currentUserDTO = new UserDTO(System.Security.Claims.ClaimsPrincipal.Current);
                if (currentUserDTO.IsValid)
                {
                    //TODO: Store DTO in session or cache
                    currentUserDTO.NotifyUserManagerAboutLoggingIn();
                    context.CurrentUser = new UserIdentity(currentUserDTO);
                }
                return null;
            });

        }
    }
}