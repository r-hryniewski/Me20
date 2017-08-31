using Me20.Contracts;
using Me20.Contracts.Entities;
using Me20.Shared.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Identity.Entities
{
    public class UserEntity : UserIdentityBase, IUser
    {
        public UserEntity(string id, string authenticationType) : base(id, authenticationType)
        {
        }
    }
}
