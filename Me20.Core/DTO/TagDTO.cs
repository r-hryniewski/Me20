using System;

namespace Me20.Core.DTO
{
    public class TagDTO : IEquatable<TagDTO>
    {
        public readonly string TagName;
        public readonly bool TagedByUser;

        public TagDTO()
        {

        }

        public TagDTO(string tagName, bool taggedByuser) : base()
        {
            TagName = tagName;
            TagedByUser = taggedByuser;
        }


        public override bool Equals(object obj)
        {
            if (obj is TagDTO)
                return this.Equals((TagDTO)obj);
            return base.Equals(obj);
        }

        public bool Equals(TagDTO other) => this.TagName.Equals(other.TagName, StringComparison.OrdinalIgnoreCase) && this.TagedByUser == other.TagedByUser;

        public override int GetHashCode()
        {
            return TagName.GetHashCode() * TagedByUser.GetHashCode();
        }
    }
}
