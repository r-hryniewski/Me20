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
        //private readonly IEnumerable<IDispatch<ContentEntity>> dispatchers;

        public ExternalContentModule(/*IDispatch<ContentEntity>[] _dispatchers*/) : base("/api/external/content")
        {
            //dispatchers = _dispatchers;

            //Post["/"] = p =>
            //{
            //    return Response.AsJson(this.Bind<ContentEntity>()
            //        .AllowAnonymous()
            //        .WithSpecific(dispatchers,
            //            AddContentDispatcher.Name)
            //        .DispatchAll(string.Empty));
            //};

            //Get["/tagged/", true] = async (p, ct) =>
            //{
            //    return Response.AsJson(await this.Bind<GetTaggedContentQuery>().ExecuteAsync(ct));
            //};
        }
    }
}