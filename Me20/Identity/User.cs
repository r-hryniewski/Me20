using Nancy.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Me20.Core;
using Me20.Core.Messages;
using Akka.Actor;

namespace Me20.Web.Identity
{
    public class User : IUserIdentity
    {
        //TODO: ActorRef

        //TODO: NYI
        public IEnumerable<string> Claims { get; } = Enumerable.Empty<string>();

        public string UserName => $"{AuthenticationType}//{Id}";

        public string Id { get; set; }

        internal string FullName { get; private set; }
        internal string FirstName { get; private set; }
        internal string LastName { get; private set; }

        internal string Email { get; private set; }

        internal string Gender { get; private set; }

        internal string AuthenticationType { get; private set; }
        
        internal User(ClaimsPrincipal currentClaimsPrincipal)
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

        private User(){}
    }
}