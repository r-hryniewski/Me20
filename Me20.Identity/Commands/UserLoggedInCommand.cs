using Me20.Common.Interfaces;
using Me20.Identity.Abstracts;

namespace Me20.Identity.Commands
{
    public class UserLoggedInCommand : AuthenthicationInfoBase, IHaveUserName
    {
        public string AuthenthicationType { get; private set; }

        public UserLoggedInCommand(string authenticationType, string id) : base(authenticationType, id)
        {

        }
    }
}
