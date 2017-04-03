using Me20.Common.DTO;
using System;
using System.Collections.Generic;

namespace Me20.Common.Interfaces
{
    public interface IEnquire<T> where T : IEntity
    {
        TimeSpan AcceptableTimeout { get; }
        IEnquire<T> QueryFor(IQuery<T> query);
        IEnquire<T> QueryForAll(params IQuery<T>[] queries);
        IEnquire<T> QueryForAll(IEnumerable<IQuery<T>> queries);

        IEnquire<T> WaitFor(TimeSpan timeout);
        HttpResult<IEnumerable<T>> Execute();
    }
}
