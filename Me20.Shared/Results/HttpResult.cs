using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Me20.Shared.Results
{
    public class HttpResult<T>
    {
        public T Item { get; private set; }
        public int StatusCode { get; private set; }
        public string[] ErrorMessages { get; private set; }

        public HttpResult(T item, int statusCode)
        {
            Item = item;
            StatusCode = statusCode;
            ErrorMessages = null;
        }

        public HttpResult(T item, HttpStatusCode status = HttpStatusCode.OK) : this(item, (int)status)
        {}

        public static HttpResult<T> CreateErrorResult(int statusCode = 400, params string[] errorMessages)
        {
            return new HttpResult<T>()
            {
                Item = default(T),
                StatusCode = statusCode,
                ErrorMessages = errorMessages ?? new string[0]
            };
        }

        public static HttpResult<T> CreateErrorResult(HttpStatusCode status = HttpStatusCode.BadRequest, params string[] errorMessages) => CreateErrorResult((int)status, errorMessages);

        public static HttpResult<T> CreateErrorResult(IEnumerable<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest) => CreateErrorResult((int)status, errorMessages?.ToArray());

        public static HttpResult<T> CreateErrorResult(IEnumerable<string> errorMessages, int statusCode = 400) => CreateErrorResult(statusCode, errorMessages?.ToArray());

        private HttpResult(){}
    }
}
