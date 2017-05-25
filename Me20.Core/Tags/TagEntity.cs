using System;
using Me20.Common.Abstracts;
using Me20.Common.DTO;

namespace Me20.Core.Tags
{
    public class TagEntity : EntityBase<TagEntity>
    {
        public string TagName { get; set; }

        public override string Uid => TagName;

        public TagEntity() : base()
        {
        }

        public override HttpResult<TagEntity> DispatchAll(string userName)
        {
            if (TagName.Length > 25)
                return HttpResult<TagEntity>.CreateErrorResult(400, "Tag name maximum length is 25 characters");
            return base.DispatchAll(userName);
        }
    }
}
