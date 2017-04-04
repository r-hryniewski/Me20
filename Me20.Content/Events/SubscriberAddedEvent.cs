using Me20.Common.Interfaces;

namespace Me20.Content.Events
{
    public class SubscriberAddedEvent : IHaveUserName
    {
        public string UserName { get; private set; }

        public SubscriberAddedEvent(string userName)
        {
            UserName = userName;
        }
    }
}
