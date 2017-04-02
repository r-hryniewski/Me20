using Me20.Common.Abstracts;
using Me20.Common.Interfaces;

namespace Me20.Common.Commands
{
    public class SubscribeToTagCommand : CommandBase, IHaveUserName
    {
        public string UserName { get; private set; }
        public string TagName { get; private set; }

        public SubscribeToTagCommand(string userName, string tagName) : base()
        {
            UserName = userName;
            TagName = tagName;
        }
    }
}
