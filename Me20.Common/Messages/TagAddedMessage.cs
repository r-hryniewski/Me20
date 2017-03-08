namespace Me20.Common.Messages
{
    public class TagAddedMessage
    {
        public string ByUserName { get; set; }
        public string TagName { get; set; }

        public TagAddedMessage(string userName, string tagName)
        {
            ByUserName = userName;
            TagName = tagName;
        }
    }
}
