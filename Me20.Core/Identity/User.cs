using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Interfaces;
using Me20.Core.Interfaces;
using Me20.Identity.Abstracts;
using Me20.Identity.Interfaces;
using System.Security.Claims;

namespace Me20.Core.Identity
{
    public class User : HaveDispatchersBase<User>, IHaveActorAddress, IHaveUserData, IHaveUserName
    {
        public ActorSelection Actor => IsValid ? ActorModel.GetUserActorSelection(UserName) : null;

        private readonly UserState state;
        public User(ClaimsPrincipal currentClaimsPrincipal)
        {
            state = new UserState(currentClaimsPrincipal);
        }

        public string UserName => state.UserName;
        public string Id => state.Id;
        public string FullName => state.FullName;
        public string FirstName => state.FirstName;
        public string LastName => state.LastName;
        public string Email => state.Email;
        public string Gender => state.Gender;
        public string AuthenticationType => state.AuthenticationType;
        public bool IsValid => state.IsValid;

        private sealed class UserState : UserDataBase
        {
            internal UserState(ClaimsPrincipal currentClaimsPrincipal) : base()
            {
                this.AuthenticationType = currentClaimsPrincipal.Identity.AuthenticationType;

                this.Id = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;

                this.FullName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? string.Empty;
                this.FirstName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
                this.LastName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;

                this.Email = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? string.Empty;

                this.Gender = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender")?.Value ?? string.Empty;
            }
        }
    }
}