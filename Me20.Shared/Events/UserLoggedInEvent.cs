using Me20.Contracts;
using Me20.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Events
{
    public class UserLoggedInEvent : IUserLoggedInEvent
    {
        public string AuthenticationType { get; private set; }
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public bool IsValid { get; private set; }

        public UserLoggedInEvent(IUserIdentity userIdentity)
        {
            AuthenticationType = userIdentity.AuthenticationType;
            Id = userIdentity.Id;
            UserName = userIdentity.UserName;
            IsValid = userIdentity.IsValid;
        }
    }
}
