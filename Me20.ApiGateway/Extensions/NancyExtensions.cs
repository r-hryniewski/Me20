using Me20.Shared.Results;
using Nancy;

namespace Me20.ApiGateway.Extensions
{
    public static class NancyExtensions
    {
        public static Response AsJson<TModel>(this IResponseFormatter formatter, TModel model, int statusCode) => formatter.AsJson(model, (HttpStatusCode)statusCode);

        public static Response AsJson<TModel>(this IResponseFormatter formatter, HttpResult<TModel> result) => formatter.AsJson(result, (HttpStatusCode)result.StatusCode);
    }
}