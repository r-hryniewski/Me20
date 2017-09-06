using Nancy;
using System.Collections.Generic;
using Me20.ApiGateway.Extensions;
//using Me20.Common.Interfaces;
//using Me20.Core.Contents;
using Nancy.ModelBinding;

namespace Me20.ApiGateway.Modules.Api
{
    public class ExternalContentModule : NancyModule
    {
        public ExternalContentModule() : base("/api/external/content")
        {

            //Get["/tagged/", true] = async (p, ct) =>
            //{
            //    return Response.AsJson(await this.Bind<GetTaggedContentQuery>().ExecuteAsync(ct));
            //};
        }
    }
}