using System;
namespace Me20.Common.Messages
{
    public class CreateTagIfNotExistsMessage
    {
        public string TagName { get; private set; }

        public CreateTagIfNotExistsMessage(string tagName)
        {
            TagName = tagName;
        }
    }
}
