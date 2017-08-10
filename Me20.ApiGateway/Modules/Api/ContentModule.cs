using Nancy;
using System.Collections.Generic;
using Me20.ApiGateway.Extensions;
//using Me20.Common.Interfaces;
//using Me20.Core.Contents;
using Nancy.ModelBinding;

namespace Me20.ApiGateway.Modules.Api
{
    public class ContentModule : NancyModule
    {
        public ContentModule() : base("/api/content")
        {
            //Get["/", true] = async (p, ct) => Response.AsJson(await this.Bind<GetUserContentQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));

            //Get["/details/", true] = async (p, ct) => Response.AsJson(await this.Bind<GetContentDetailsQuery>().ExecuteAsync(Context.CurrentUser.UserName, ct));

            //Post["/"] = p => Response.AsJson(this.Bind<AddContentCommand>());

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