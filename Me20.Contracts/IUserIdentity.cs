﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface IUserIdentity : IEntity, IHaveUserName, ICanBeValidated
    {
        string AuthenticationType { get; }
    }
}