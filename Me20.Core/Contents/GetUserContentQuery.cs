using Akka.Actor;
using Me20.Common.Interfaces;
using Me20.Core.DTO;
using Me20.Identity.QueryMessages;
using Me20.Identity.QueryResultMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using Me20.Common.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Core.Contents
{
    public class GetUserContentQuery : IQuery<IEnumerable<ContentEntity>>
    {
        public int Page { get; set; }

        public async Task<HttpResult<IEnumerable<ContentEntity>>> ExecuteAsync(string userName, CancellationToken ct)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(401);

                if (await ActorModel.UsersManagerActorRef.Ask(new GetUserContentQueryMessage(userName, Page), ct) is GetUserContentQueryResultMessage result)
                {
                    return new HttpResult<IEnumerable<ContentEntity>>(result.ContentWithTags.Select(cwt =>
                    new ContentEntity
                    {
                        Url = cwt.Key.ToString(),
                        Tags = cwt.Value.Select(v => new TagDTO(v, true)).ToList()
                    }
                ));
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{nameof(GetUserContentQuery)} exception");
            }
            return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(500);
        }
    }
}
