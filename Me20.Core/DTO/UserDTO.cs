using Akka.Actor;
using Me20.Core.Helpers;
using Me20.Core.Interfaces;
using Me20.Identity.Abstracts;
using Me20.Identity.Messages;
using System.Security.Claims;

namespace Me20.Core.DTO
{
    public class UserDTO : UserDataBase, IHaveActorAddress
    {
        public ActorSelection Actor => IsValid ? ActorModelHelper.GetUserActorSelection(UserName) : null;

        public UserDTO(ClaimsPrincipal currentClaimsPrincipal) : base()
        {
            this.AuthenticationType = currentClaimsPrincipal.Identity.AuthenticationType;

            this.Id = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;

            this.FullName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? string.Empty;
            this.FirstName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
            this.LastName = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;

            this.Email = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? string.Empty;

            this.Gender = currentClaimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender")?.Value ?? string.Empty;
        }

        public void NotifyUserManagerAboutLoggingIn()
        {
            ActorModel.UsersManagerActorRef.Tell(new UserLoggedInMessage(
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
        

        private UserDTO(){}
    }
}