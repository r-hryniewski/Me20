using Me20.Common.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Common.Interfaces
{
    public interface IQuery<T>
    {
        Task<HttpResult<T>> ExecuteAsync(string userName, CancellationToken ct);
    }

    public interface IAnonymousQuery<T>
    {
        Task<HttpResult<T>> ExecuteAsync(CancellationToken ct);
    }
}
