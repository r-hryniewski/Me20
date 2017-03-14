using Me20.Common.DTO;
using Me20.Core.Interfaces;
using Me20.Core.Tags;
using Nancy;

namespace Me20.Web.Modules.Api
{
    public class TagsModule : NancyModule
    {
        IDispatch<Tag> Dispatcher { get; set; }
        public TagsModule(IDispatch<Tag> dispatcher) : base("/api/tags")
        {
            Dispatcher = dispatcher;

            //TODO: Change it to post after doing some frontend
            Get["/{tagName}"] = p =>
            {
                HttpResult<Tag> result = dispatcher.Subsribe(p, Context.CurrentUser.UserName);

                //TODO: Clean it up - Status codes;
                return Response.AsJson(result.Item, (HttpStatusCode)result.StatusCode);
            };
        }
    }
}