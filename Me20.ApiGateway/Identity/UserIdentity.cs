using Me20.Contracts;
using Nancy.Security;
using System.Collections.Generic;
using System.Linq;

namespace Me20.ApiGateway.Identity
{
    public class UserIdentity : IUserIdentity, IHaveUserName
    {
        //TODO: NYI
        public IEnumerable<string> Claims => Enumerable.Empty<string>();

        public string UserName { get; private set; }

        public UserIdentity(IHaveUserName user)
        {
            UserName = user.UserName;
        }
        private UserIdentity() { }

        public readonly static IUserIdentity Empty = new UserIdentity() { UserName = string.Empty };
    }
}