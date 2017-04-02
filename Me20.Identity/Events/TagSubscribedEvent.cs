namespace Me20.Identity.Events
{
    internal class TagSubscribedEvent
    {
        internal string TagName { get; private set; }
        public TagSubscribedEvent(string tagName)
        {
            TagName = tagName;
        }
    }
}
