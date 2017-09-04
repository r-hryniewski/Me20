using Me20.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Utilities
{
    public class EntityComparer : IComparer<IEntity>, IEqualityComparer<IEntity>
    {
        public static readonly EntityComparer Instance = new EntityComparer();

        private readonly StringComparer idComparer;

        public EntityComparer()
        {
            idComparer = StringComparer.Ordinal;
        }

        public int Compare(IEntity x, IEntity y)
        {
            return idComparer.Compare(x.Id, x.Id);
        }

        public bool Equals(IEntity x, IEntity y)
        {
            return idComparer.Equals(x.Id, y.Id);
        }

        public int GetHashCode(IEntity obj)
        {
            return idComparer.GetHashCode(obj.Id);
        }
    }
}
