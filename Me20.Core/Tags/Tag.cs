using Me20.Common.Abstracts;

namespace Me20.Core.Tags
{
    public class Tag : HaveDispatchersBase<Tag>
    {
        public string TagName { get; set; }

        public Tag(string tagName) : base()
        {
            TagName = tagName;
        }
    }
}
