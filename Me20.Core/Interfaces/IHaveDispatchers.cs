using Me20.Common.DTO;
using System.Collections.Generic;

namespace Me20.Core.Interfaces
{
    public interface IHaveDispatchers<T>
    {
        //TODO: T As param after choosing frontend framework and implementing posts and model bindings
        T With(IDispatch<T> dispatchers);
        T With(IEnumerable<IDispatch<T>> dispatchers);
        T WithSpecific(IEnumerable<IDispatch<T>> dispatchers, params string[] internalNames);

        HttpResult<T> DispatchAll(string userName);
    }
}
