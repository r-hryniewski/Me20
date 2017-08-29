using Me20.Common.Interfaces;
using System;

namespace Me20.Content.QueryMessages
{
    public class GetTaggedContentQueryMessage : IHaveContentTag
    {
        public string ContentTag { get; private set; }
        public GetTaggedContentQueryMessage(string tag)
        {
            ContentTag = tag;
        }

    }
}
