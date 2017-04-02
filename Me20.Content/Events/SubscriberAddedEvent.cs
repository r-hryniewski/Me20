using Me20.Common.Interfaces;

namespace Me20.Content.Events
{
    internal class SubscriberAddedEvent : IHaveUserName
    {
        public string UserName { get; private set; }

        internal SubscriberAddedEvent(string userName)
        {
            UserName = userName;
        }
    }
}
