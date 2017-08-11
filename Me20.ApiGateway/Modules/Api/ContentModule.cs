using Nancy;
using System.Collections.Generic;
using Me20.ApiGateway.Extensions;
using Nancy.ModelBinding;
using Me20.Contracts.Commands;
using Me20.Contracts;

namespace Me20.ApiGateway.Modules.Api
{
    public class ContentModule : NancyModule
    {
        public ContentModule(IHandleCommands<Commands.AddContent> addContentCmdHandler) : base("/api/my/content")
        {
            Post["/", true] = async (p, ct) => Response.AsJson(await addContentCmdHandler.Handle(this.Bind<Commands.AddContent>(), ct));


            //Get["/", true] = async (p, ct) => Response.AsJson(await this.Bind<GetUserContentQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));

            //Get["/details/", true] = async (p, ct) => Response.AsJson(await this.Bind<GetContentDetailsQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));

            //Post["/rate"] = p => Response.AsJson(this.Bind<ContentEntity>()
            //        .WithSpecific(dispatchers,
            //            RateContentDispatcher.Name)
            //        .DispatchAll(Context.CurrentUser.UserName));

            //Post["/tag"] = p => Response.AsJson(this.Bind<ContentEntity>()
            //        .WithSpecific(dispatchers,
            //            TagContentDispatcher.Name)
            //        .DispatchAll(Context.CurrentUser.UserName));

            //Delete["/"] = p => Response.AsJson(this.Bind<ContentEntity>()
            //        .WithSpecific(dispatchers,
            //            RemoveUserContentDispatcher.Name)
            //        .DispatchAll(Context.CurrentUser.UserName));

            //Put["/"] = p => Response.AsJson(this.Bind<ContentEntity>()
            //        .WithSpecific(dispatchers,
            //            RenameUserContentDispatcher.Name)
            //        .DispatchAll(Context.CurrentUser.UserName));
        }
    }
}