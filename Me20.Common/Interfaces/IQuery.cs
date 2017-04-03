using System;
using System.Collections.Generic;

namespace Me20.Common.Interfaces
{
    public interface IQuery<T> where T : IEntity
    { 
        IEnumerable<T> Execute(IEnquire<T> enquirer);
    }
}
