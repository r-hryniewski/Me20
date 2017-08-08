using Me20.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Abstracts
{
    public abstract class UserIdentityBase : IUserIdentity
    {
        public virtual string Id { get; protected set; }
        public virtual string AuthenticationType { get; protected set; }

        public virtual string UserName => !string.IsNullOrEmpty(AuthenticationType) && !string.IsNullOrEmpty(Id) ? $"{AuthenticationType}-{Id}" : string.Empty;

        public bool IsValid => !string.IsNullOrEmpty(UserName);

        protected UserIdentityBase(string id, string authenticationType)
        {
            Id = id ?? string.Empty;
            AuthenticationType = authenticationType ?? string.Empty;
        }
    }
}
