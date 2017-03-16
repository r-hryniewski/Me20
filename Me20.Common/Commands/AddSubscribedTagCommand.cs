using Me20.Common.Abstracts;

namespace Me20.Common.Commands
{
    public class AddSubscribedTagCommand : CommandBase
    {
        public string TagName { get; private set; }

        public AddSubscribedTagCommand(string tagName) : base()
        {
            TagName = tagName;
        }
    }
}
