using Me20.Contracts.Entities;
using Me20.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.Entities
{
    public class TagEntity : ITag
    {
        public string Id => this.TagNameToId();
        public string TagName { get; private set; }

        public TagEntity(string tagName)
        {
            TagName = tagName;
        }
    }
}
