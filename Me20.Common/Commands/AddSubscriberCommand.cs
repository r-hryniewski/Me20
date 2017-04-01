using Me20.Common.Abstracts;
using Me20.Common.Interfaces;

namespace Me20.Common.Commands
{
    public class AddSubscriberCommand : CommandBase, IHaveUserName
    {
        public string UserName { get; private set; }
        public string TagName { get; private set; }

        //TODO: Make it more reusable?
        //Senddee name/ address path as parameter?
        public AddSubscriberCommand(string userName, string tagName) : base()
        {
            UserName = userName;
            TagName = tagName;
        }
    }
}
