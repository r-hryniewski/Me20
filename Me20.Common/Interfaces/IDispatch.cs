namespace Me20.Common.Interfaces
{
    public interface IDispatch<T> : IHaveInternalName
    {
        void Dispatch(T item, string userName);
    }
}
