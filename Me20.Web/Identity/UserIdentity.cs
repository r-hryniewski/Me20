using Me20.Common.Interfaces;
using Nancy.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;

namespace Me20.Web.Identity
{
    public class UserIdentity : IUserIdentity, IHaveUserName, IEntity
    {
        //TODO: NYI
        public IEnumerable<string> Claims => Enumerable.Empty<string>();

        public string UserName { get; private set; }
        public string Uid => UserName;

        private string AuthenticationType { get; set; }
        private string Id { get; set; }

        private UserIdentity(){}

        public UserIdentity(ClaimsPrincipal claimsPrincipal)
        {
            Id = claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;
            AuthenticationType = claimsPrincipal.Identity.AuthenticationType;
        }

        public readonly static IUserIdentity Empty = new UserIdentity() { UserName = string.Empty };
    }
}