using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Common.Messages
{
    public class UserLoggedInMessage
    {

        public string UserName { get; private set; }

        public string Id { get; private set; }

        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Gender { get; private set; }

        public string AuthenticationType { get; private set; }

        public UserLoggedInMessage(string userName, string id, string fullName, string firstName, string lastName, string email, string gender, string authenticationType)
        {
            this.UserName = userName;
            this.Id = id;
            this.FullName = fullName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Gender = gender;
            this.AuthenticationType = authenticationType;
        }
    }
}
