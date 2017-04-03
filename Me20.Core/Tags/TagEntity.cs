using System;
using Me20.Common.Abstracts;

namespace Me20.Core.Tags
{
    public class TagEntity : EntityBase<TagEntity>
    {
        public string TagName { get; set; }

        public override string Uid => TagName;

        public TagEntity() : base()
        {
        }
    }
}
