namespace Me20.Identity.Events
{
    internal class TagSubscribedEvent
    {
        internal string TagName { get; private set; }
        internal TagSubscribedEvent(string tagName)
        {
            TagName = tagName;
        }
    }
}
