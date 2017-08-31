using Me20.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.IdentityActors.Commands
{
    public class CreateNewUserCommand : ICreateNewUserCommand
    {
        public string AuthenticationType { get; private set; }
        public string Id { get; private set; }

        public CreateNewUserCommand(string authenticationType, string id)
        {
            AuthenticationType = authenticationType;
            Id = id;
        }
    }
}
