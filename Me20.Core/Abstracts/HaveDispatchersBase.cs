using System.Collections.Generic;
using Me20.Common.DTO;
using Me20.Core.Interfaces;
using Me20.Common.Comparers;
using System.Linq;

namespace Me20.Core.Abstracts
{
    public abstract class HaveDispatchersBase<T> : IHaveDispatchers<T> where T : HaveDispatchersBase<T>
    {
        protected readonly HashSet<IDispatch<T>> dispatchers;

        protected HaveDispatchersBase()
        {
            dispatchers = new HashSet<IDispatch<T>>(new InternalNameEqualityComparer());
        }

        public T With(IDispatch<T> dispatcher)
        {
            dispatchers.Add(dispatcher);
            return (T)this;
        }

        public T With(IEnumerable<IDispatch<T>> dispatchers)
        {
            foreach (var dispatcher in dispatchers)
                this.With(dispatcher);

            return (T)this;
        }

        public T WithSpecific(IEnumerable<IDispatch<T>> dispatchers, params string[] internalNames) => this.With(
            dispatchers.Join(
                inner: internalNames,
                outerKeySelector: dispatcher => dispatcher.InternalName,
                innerKeySelector: name => name,
                resultSelector: (dispatcher, name) => dispatcher));

        public abstract HttpResult<T> DispatchAll(string userName);
    }
}
