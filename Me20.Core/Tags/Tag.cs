using Me20.Common.DTO;
using System.Net;
using Me20.Common.Abstracts;

namespace Me20.Core.Tags
{
    public class Tag : HaveDispatchersBase<Tag>
    {
        public string TagName { get; private set; }

        public Tag(string tagName) : base()
        {
            TagName = tagName;
        }

        public override HttpResult<Tag> DispatchAll(string userName)
        {
            foreach (var dispatcher in dispatchers)
                dispatcher.Dispatch(this, userName);

            return new HttpResult<Tag>(this, HttpStatusCode.Accepted);
        }
    }
}
