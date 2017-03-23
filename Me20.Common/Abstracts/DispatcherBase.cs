using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Abstracts
{
    public abstract class DispatcherBase<T> : IDispatch<T>
    {
        public string InternalName { get; private set; }
        public abstract void Dispatch(T item, string userName);

        protected DispatcherBase()
        {
            var typeName = this.GetType().Name;
            var dispatcherIndex = typeName.LastIndexOf("Dispatcher");
            if (dispatcherIndex > 0)
                InternalName = typeName.Remove(dispatcherIndex);
            else
                InternalName = typeName;
        }

        protected virtual void ValidateAndExecute(T item, string userName, params Action[] actions)
        {
            if (!string.IsNullOrEmpty(userName))
                foreach (var action in actions)
                    action();
        }
    }
}
