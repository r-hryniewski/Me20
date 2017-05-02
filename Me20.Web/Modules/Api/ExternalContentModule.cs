using Nancy;
using System.Collections.Generic;
using Me20.Web.Extensions;
using Me20.Common.Interfaces;
using Me20.Core.Contents;
using Nancy.ModelBinding;

namespace Me20.Web.Modules.Api
{
    public class ExternalContentModule : NancyModule
    {
        private readonly IEnumerable<IDispatch<ContentEntity>> dispatchers;

        public ExternalContentModule(IDispatch<ContentEntity>[] _dispatchers) : base("/api/external/content")
        {
            dispatchers = _dispatchers;

            Post["/"] = p =>
            {
                var content = this.Bind<ContentEntity>();
                return Response.AsJson(content
                    .AllowAnonymous()
                    .WithSpecific(dispatchers,
                        AddContentDispatcher.Name)
                    .DispatchAll(string.Empty));
            };

            Get["/suggested/"] = p =>
            {
                var enquirer = this.Bind<ContentDetailsEnquirer>();
                return Response.AsJson(enquirer
                    .QueryForAll(new GetUserContentDetailsQuery(Context.CurrentUser.UserName, enquirer.Uri),
                        new GetContentDetailsQuery(Context.CurrentUser.UserName, enquirer.Uri))
                    .Execute());
            };
        }
    }
}