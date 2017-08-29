//using Me20.Core.Tags;
using Nancy;
using System.Collections.Generic;
using Me20.ApiGateway.Extensions;
//using Me20.Common.Interfaces;
using Nancy.ModelBinding;

namespace Me20.ApiGateway.Modules.Api
{
    public class TagsModule : NancyModule
    {
        //private readonly IEnumerable<IDispatch<TagEntity>> dispatchers;

        public TagsModule(/*IDispatch<TagEntity>[] _dispatchers*/) : base("/api/tags")
        {
            //dispatchers = _dispatchers;

            //Post["/"] = p =>
            //{
            //    return Response.AsJson(this.Bind<TagEntity>()
            //        .WithSpecific(dispatchers,
            //        //Not Used at the moment
            //        //CreateTagIfNotExistsDispatcher.Name,
            //        TagSubscribedDispatcher.Name)
            //        .DispatchAll(Context.CurrentUser.UserName));
            //};

            //Get["/", true] = async (p, ct) =>  Response.AsJson(await this.Bind<GetAllTagNamesForUserQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));
        }
    }
}