﻿using Me20.Core.Identity;
using Me20.Common.Interfaces;
using Nancy.Security;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Web.Identity
{
    public class UserIdentity : IUserIdentity, IHaveUserName
    {
        //TODO: NYI
        public IEnumerable<string> Claims => Enumerable.Empty<string>();

        public string UserName { get; private set; }

        public UserIdentity(User user)
        {
            UserName = user.UserName;
        }
    }
}