using Me20.Contracts;
using Nancy.Security;
using System.Collections.Generic;
using System.Linq;
using System;
using Me20.Shared.Abstracts;
using System.Security.Claims;

namespace Me20.ApiGateway.Identity
{
    public class UserIdentity : UserIdentityBase, Nancy.Security.IUserIdentity
    {
        //TODO: NYI
        public IEnumerable<string> Claims => Enumerable.Empty<string>();

        public UserIdentity(ClaimsPrincipal claimsPrincipal) : base(
            id: claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value,
            authenticationType: claimsPrincipal.Identity.AuthenticationType)
        {
        }
    }
}