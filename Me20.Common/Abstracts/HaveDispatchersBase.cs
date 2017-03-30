using System.Collections.Generic;
using Me20.Common.DTO;
using Me20.Common.Interfaces;
using Me20.Common.Comparers;
using System.Linq;

namespace Me20.Common.Abstracts
{
    public abstract class HaveDispatchersBase<T> : IHaveDispatchers<T> where T : HaveDispatchersBase<T>
    {
        protected readonly HashSet<IDispatch<T>> dispatchers;

        protected HaveDispatchersBase()
        {
            dispatchers = new HashSet<IDispatch<T>>(new InternalNameEqualityComparer());
        }

        public virtual T With(IDispatch<T> dispatcher)
        {
            dispatchers.Add(dispatcher);
            return (T)this;
        }

        public virtual T With(IEnumerable<IDispatch<T>> dispatchers)
        {
            foreach (var dispatcher in dispatchers)
                this.With(dispatcher);

            return (T)this;
        }

        public virtual T WithSpecific(IEnumerable<IDispatch<T>> dispatchers, params string[] internalNames) => this.With(
            dispatchers.Join(
                inner: internalNames,
                outerKeySelector: dispatcher => dispatcher.InternalName,
                innerKeySelector: name => name,
                resultSelector: (dispatcher, name) => dispatcher));

        public virtual HttpResult<T> DispatchAll(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return HttpResult<T>.CreateErrorResult(401, "UserName is empty, you're not authenthicated in any way.");

            foreach (var dispatcher in dispatchers)
                dispatcher.Dispatch((T)this, userName);

            return new HttpResult<T>((T)this, System.Net.HttpStatusCode.Accepted);
        }
    }
}
