using Nancy;
using System.Collections.Generic;
using Me20.Web.Extensions;
using Me20.Common.Interfaces;
using Me20.Core.Contents;

namespace Me20.Web.Modules.Api
{
    public class ContentModule : NancyModule
    {
        private readonly IEnumerable<IDispatch<Content>> dispatchers;

        public ContentModule(IDispatch<Content>[] _dispatchers) : base("/api/content")
        {
            dispatchers = _dispatchers;

            //TODO: Change it to post after doing some frontend
            Get["/{contentUrl}"] = p =>
            {
                return Response.AsJson(new Content(p.contentUrl, System.Linq.Enumerable.Empty<string>())
                    .WithSpecific(dispatchers,
                    CreateContentIfNotExistsDispatcher.Name,
                    AddContentDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };
        }
    }
}