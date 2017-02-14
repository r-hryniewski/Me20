using Nancy.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;

namespace Me20.Web.Identity
{
    public class User : IUserIdentity
    {
        //TODO: ActorRef
        //TODO: NYI
        public IEnumerable<string> Claims { get; } = Enumerable.Empty<string>();

        public string Id { get; set; }

        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Gender { get; private set; }

        public string AuthenthicationType { get; private set; }
        
        //TODO: Proper persistence and retrieval (and some kind of cache?)
        public User(ClaimsPrincipal currentClaimsPrincipal)
        {
            //TODO: Proper source
            this.AuthenthicationType = currentClaimsPrincipal.Identity.AuthenticationType;

            this.Id = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;

            //TODO: Some internal guid returned after persistence/retrieval?
            this.UserName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? string.Empty;
            this.FirstName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
            this.LastName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;

            this.Email = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? string.Empty;

            this.Gender = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender")?.Value ?? string.Empty;
        }

        public void NotifyUserManagerAboutLoggingIn()
        {

        }

        private User(){}
    }
}