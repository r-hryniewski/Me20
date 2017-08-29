using Me20.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;
using Newtonsoft.Json;

namespace Me20.Shared.Results
{
    public class HttpResult<TItem> : IResult<TItem>
    {
        public TItem Item { get; private set; }
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public string[] ErrorMessages { get; private set; }
        IEnumerable<string> IResult.ErrorList => ErrorMessages;

        [JsonIgnore]
        bool IResult.Successful => ErrorMessages == null || ErrorMessages.Length == 0;

        public HttpResult(TItem item, int statusCode)
        {
            Item = item;
            StatusCode = statusCode;
            ErrorMessages = null;
        }

        public HttpResult(TItem item, HttpStatusCode status = HttpStatusCode.OK) : this(item, (int)status)
        {}

        public static HttpResult<TItem> CreateErrorResult(int statusCode = 400, params string[] errorMessages)
        {
            return new HttpResult<TItem>()
            {
                Item = default(TItem),
                StatusCode = statusCode,
                ErrorMessages = errorMessages ?? new string[0]
            };
        }

        public static HttpResult<TItem> CreateErrorResult(HttpStatusCode status = HttpStatusCode.BadRequest, params string[] errorMessages) => CreateErrorResult((int)status, errorMessages);

        public static HttpResult<TItem> CreateErrorResult(IEnumerable<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest) => CreateErrorResult((int)status, errorMessages?.ToArray());

        public static HttpResult<TItem> CreateErrorResult(IEnumerable<string> errorMessages, int statusCode = 400) => CreateErrorResult(statusCode, errorMessages?.ToArray());

        private HttpResult(){}
    }
}
