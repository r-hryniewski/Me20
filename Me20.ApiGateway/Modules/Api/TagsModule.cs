using Nancy;
using System.Collections.Generic;
using Me20.ApiGateway.Extensions;
using Nancy.ModelBinding;
using Me20.Contracts;

namespace Me20.ApiGateway.Modules.Api
{
    public class TagsModule : NancyModule
    {
        public TagsModule(IHandleCommands<Commands.SubscribeToTagCommand> addContentCmdHandler) : base("/api/tags")
        {
            Post["/", true] = async (p, ct) => Response.AsJson((await addContentCmdHandler.Handle(this.Bind<Commands.SubscribeToTagCommand>(), ct)));

            //Get["/", true] = async (p, ct) =>  Response.AsJson(await this.Bind<GetAllTagNamesForUserQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));
        }
    }
}