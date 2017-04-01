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

            Post["/"] = p =>
            {
                var tag = this.Bind<Tag>();
                return Response.AsJson(tag
                    .WithSpecific(dispatchers,
                    //Not Used at the moment
                    //CreateTagIfNotExistsDispatcher.Name,
                    TagSubscribedDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };
        }
    }
}