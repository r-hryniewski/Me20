using Me20.Core.Tags;
using Nancy;
using System.Collections.Generic;
using Me20.Web.Extensions;
using Me20.Common.Interfaces;
using Nancy.ModelBinding;

namespace Me20.Web.Modules.Api
{
    public class TagsModule : NancyModule
    {
        private readonly IEnumerable<IDispatch<Tag>> dispatchers;

        public TagsModule(IDispatch<Tag>[] _dispatchers) : base("/api/tags")
        {
            dispatchers = _dispatchers;

            //TODO: Change it to post after doing some frontend
            //Get["/{tagName}"] = p =>
            //{
            //    return Response.AsJson(new Tag(p.tagName)
            //        .WithSpecific(dispatchers,
            //        CreateTagIfNotExistsDispatcher.Name, TagSubscribedDispatcher.Name)
            //        .DispatchAll(Context.CurrentUser.UserName));
            //};

            Post["/"] = p =>
            {
                var tag = this.Bind<Tag>();
                return Response.AsJson(tag
                    .WithSpecific(dispatchers,
                    CreateTagIfNotExistsDispatcher.Name, TagSubscribedDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };
        }
    }
}