using Me20.Common.Interfaces;
using System.Collections.Generic;

namespace Me20.Common.Comparers
{
    public class InternalNameEqualityComparer : IEqualityComparer<IHaveInternalName>
    {
        public bool Equals(IHaveInternalName x, IHaveInternalName y) => x.InternalName == y.InternalName;

        public int GetHashCode(IHaveInternalName obj) => obj.InternalName.GetHashCode();
    }
}
