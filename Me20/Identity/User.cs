using Akka.Actor;
using Me20.Core;
using Me20.Core.Helpers;
using Me20.Core.Interfaces;
using Me20.Identity.Interfaces;
using Me20.Identity.Messages;
using Nancy.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Me20.Web.Identity
{
    public class User : IUserIdentity, IHaveUserName, IHaveUserData, IHaveActorAddress
    {
        public IEnumerable<string> Claims { get; } = Enumerable.Empty<string>();

        public string UserName => !string.IsNullOrEmpty(AuthenticationType) && !string.IsNullOrEmpty(Id) ? $"{AuthenticationType}-{Id}" : string.Empty;

        public ActorSelection Actor => IsValid ? ActorModelHelper.GetUserActorSelection(UserName) : null;

        public string Id { get; set; }

        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Gender { get; private set; }

        public string AuthenticationType { get; private set; }

        public User(ClaimsPrincipal currentClaimsPrincipal)
        {
            this.AuthenticationType = currentClaimsPrincipal.Identity.AuthenticationType;

            this.Id = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;

            this.FullName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? string.Empty;
            this.FirstName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
            this.LastName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;

            this.Email = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? string.Empty;

            this.Gender = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender")?.Value ?? string.Empty;
        }

        internal void NotifyUserManagerAboutLoggingIn()
        {
            ActorModel.UsersManagerActorRef.Tell(new UserLoggedInMessage(
                userName: this.UserName,
                id: this.Id,
                fullName: this.FullName,
                firstName: this.FirstName,
                lastName: this.LastName,
                email: this.Email,
                gender: this.Gender,
                authenticationType: this.AuthenticationType
                ));
        }
        //TODO: More validation rules?
        internal bool IsValid => string.IsNullOrEmpty(UserName);

        private User(){}
    }
}