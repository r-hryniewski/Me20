using Me20.Common.DTO;
using Nancy;

namespace Me20.Web.Extensions
{
    public static class NancyExtensions
    {
        public static Response AsJson<TModel>(this IResponseFormatter formatter, TModel model, int statusCode) => formatter.AsJson(model, (HttpStatusCode)statusCode);

        public static Response AsJson<TModel>(this IResponseFormatter formatter, HttpResult<TModel> result) => formatter.AsJson(result.Item, (HttpStatusCode)result.StatusCode);
    }
}