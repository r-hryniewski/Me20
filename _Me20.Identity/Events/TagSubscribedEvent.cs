namespace Me20.Identity.Events
{
    public class TagSubscribedEvent
    {
        public string TagName { get; private set; }
        public TagSubscribedEvent(string tagName)
        {
            TagName = tagName;
        }
    }
}
