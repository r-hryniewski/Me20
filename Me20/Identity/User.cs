using Nancy.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Me20.Web.Identity
{
    public class User : IUserIdentity
    {

        public IEnumerable<string> Claims { get; } = Enumerable.Empty<string>();

        public string UserName { get; private set; }

        public string AuthenthicationType { get; private set; }
        
        public User(IIdentity _identity)
        {
            AuthenthicationType = _identity.AuthenticationType;
            UserName = _identity.Name;
        }

        public User(){}
    }
}