using Me20.Core.Interfaces;

namespace Me20.Core.Abstracts
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
    }
}
