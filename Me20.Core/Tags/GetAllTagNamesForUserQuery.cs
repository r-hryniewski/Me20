using System.Collections.Generic;
using Me20.Common.Interfaces;
using Akka.Actor;
using Me20.Identity.QueryMessages;
using System.Linq;
using Me20.Identity.QueryResultMessages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Me20.Common.DTO;

namespace Me20.Core.Tags
{
    public class GetAllTagNamesForUserQuery : IQuery<IEnumerable<TagEntity>>
    {
        public async Task<HttpResult<IEnumerable<TagEntity>>> ExecuteAsync(string userName, CancellationToken ct)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return HttpResult<IEnumerable<TagEntity>>.CreateErrorResult(401);

                if (await ActorModel.UsersManagerActorRef.Ask(new GetAllTagNamesForUserQueryMessage(userName), ct) is GetAllTagNamesForUserQueryResultMessage result)
                    return new HttpResult<IEnumerable<TagEntity>>(result.TagNames.Select(tn => new TagEntity { TagName = tn }), 200);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{nameof(GetAllTagNamesForUserQuery)} exception");
            }
            return HttpResult<IEnumerable<TagEntity>>.CreateErrorResult(500);
        }
    }
}
