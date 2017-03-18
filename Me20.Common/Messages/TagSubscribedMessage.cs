namespace Me20.Common.Messages
{
    [System.Obsolete("Use commands")]
    public class TagSubscribedMessage
    {
        public string ByUserName { get; set; }
        public string TagName { get; set; }

        public TagSubscribedMessage(string userName, string tagName)
        {
            ByUserName = userName;
            TagName = tagName;
        }
    }
}
