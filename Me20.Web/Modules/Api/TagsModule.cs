using Me20.Core.Interfaces;
using Me20.Core.Tags;
using Nancy;
using System.Collections.Generic;
using Me20.Web.Extensions;

namespace Me20.Web.Modules.Api
{
    public class TagsModule : NancyModule
    {
        private readonly IEnumerable<IDispatch<Tag>> dispatchers;

        public TagsModule(IDispatch<Tag>[] _dispatchers) : base("/api/tags")
        {
            dispatchers = _dispatchers;

            //TODO: Change it to post after doing some frontend
            Get["/{tagName}"] = p =>
            {
                return Response.AsJson(new Tag(p.tagName)
                    .WithSpecific(dispatchers, TagSubscribedDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };
        }
    }
}