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
                return Response.AsJson(this.Bind<ContentEntity>()
                    .WithSpecific(dispatchers,
                        AddContentDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };

            Post["/rate"] = p =>
            {
                return Response.AsJson(this.Bind<ContentEntity>()
                    .WithSpecific(dispatchers,
                        RateContentDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };

            Post["/tag"] = p =>
            {
                return Response.AsJson(this.Bind<ContentEntity>()
                    .WithSpecific(dispatchers,
                        TagContentDispatcher.Name)
                    .DispatchAll(Context.CurrentUser.UserName));
            };

            Get["/", true] = async (p, ct) => Response.AsJson(await this.Bind<GetUserContentQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));

            Get["/details/", true] = async (p, ct) =>
            {
                return Response.AsJson(await this.Bind<GetContentDetailsQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));
            };
        }
    }
}