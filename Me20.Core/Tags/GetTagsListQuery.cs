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
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;

namespace Me20.Core.Tags
{
    public class GetTagsListQuery : IAnonymousQuery<IEnumerable<TagEntity>>
    {
        public async Task<HttpResult<IEnumerable<TagEntity>>> ExecuteAsync(CancellationToken ct)
        {
            try
            {
                if (await ActorModel.TagsManagerActorRef.Ask(new GetTagsListQueryMessage(), ct) is GetTagsListQueryResultMessage result)
                    return new HttpResult<IEnumerable<TagEntity>>(result.TagsList.Select(tn => new TagEntity { TagName = tn }), 200);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{nameof(GetAllTagNamesForUserQuery)} exception");
            }
            return HttpResult<IEnumerable<TagEntity>>.CreateErrorResult(500);
        }
    }
}
