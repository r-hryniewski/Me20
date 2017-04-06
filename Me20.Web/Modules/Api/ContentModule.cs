using Nancy;
using System.Collections.Generic;
using Me20.Web.Extensions;
using Me20.Common.Interfaces;
using Me20.Core.Contents;
using Nancy.ModelBinding;

namespace Me20.Web.Modules.Api
{
    public class ContentModule : NancyModule
    {
        private readonly IEnumerable<IDispatch<ContentEntity>> dispatchers;

        public ContentModule(IDispatch<ContentEntity>[] _dispatchers) : base("/api/content")
        {
            dispatchers = _dispatchers;

            //TODO: Change it to post after doing some frontend
            Post["/"] = p =>
            {
                var content = this.Bind<ContentEntity>();
                return Response.AsJson(content
                    .WithSpecific(dispatchers,
                    //CreateContentIfNotExistsDispatcher.Name,
                    AddContentDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };

            Get["/"] = p => Response.AsJson(new ContentEnquirer()
                .QueryFor(new GetUserContentQuery(Context.CurrentUser.UserName))
                .Execute());
        }
    }
}