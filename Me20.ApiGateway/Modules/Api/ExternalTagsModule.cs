using Nancy;
using Me20.ApiGateway.Extensions;
using Nancy.ModelBinding;
//using Me20.Core.Tags;

namespace Me20.ApiGateway.Modules.Api
{
    public class ExternalTagsModule : NancyModule
    {
        public ExternalTagsModule() : base("/api/external/tags")
        {
            //Get["/", true] = async (p, ct) =>
            //{
            //    return Response.AsJson(await this.Bind<GetTagsListQuery>().ExecuteAsync(ct));
            //};
        }
    }
}