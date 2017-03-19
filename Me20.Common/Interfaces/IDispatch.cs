namespace Me20.Common.Interfaces
{
    public interface IDispatch<T> : IHaveInternalName
    {
        //TODO: T As param after choosing frontend framework and implementing posts and model bindings
        void Dispatch(T item, string userName);
    }
}
