using System.Net;

namespace Me20.Common.DTO
{
    public class HttpResult<T>
    {
        public T Item { get; private set; }
        public int StatusCode { get; private set; }

        public HttpResult(T item, HttpStatusCode status = HttpStatusCode.OK)
        {
            Item = item;
            StatusCode = (int)status;
        }

    }
}
