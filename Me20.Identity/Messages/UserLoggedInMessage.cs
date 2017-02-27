using Me20.Identity.Abstracts;

namespace Me20.Identity.Messages
{
    public class UserLoggedInMessage : BaseUserData
    {
        public UserLoggedInMessage(string id, string fullName, string firstName, string lastName, string email, string gender, string authenticationType)
            : base(id, fullName, firstName, lastName, email, gender, authenticationType)
        {

        }
    }
}
