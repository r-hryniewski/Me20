using Me20.Common.Abstracts;
using Me20.Common.Interfaces;

namespace Me20.Common.Commands
{
    public class AddSubscriberCommand : CommandBase, IHaveUserName
    {
        public string UserName { get; private set; }

        public AddSubscriberCommand(string userName) : base()
        {
            UserName = userName;
        }
    }
}
