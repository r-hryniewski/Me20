using System;
namespace Me20.Common.Messages
{
    [Obsolete("Not used at the moment")]
    public class CreateTagIfNotExistsMessage
    {
        public string TagName { get; private set; }

        public CreateTagIfNotExistsMessage(string tagName)
        {
            TagName = tagName;
        }
    }
}
