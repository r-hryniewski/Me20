using Me20.Common.DTO;

namespace Me20.Core.Interfaces
{
    public interface IDispatch<T>
    {
        //TODO: T As param after choosing frontend framework and implementing posts and model bindings
        HttpResult<T> Subsribe(dynamic parameters, string userName);
    }
}
