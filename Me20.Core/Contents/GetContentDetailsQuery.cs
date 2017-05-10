using Akka.Actor;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;
using Me20.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Me20.Common.DTO;
using System.Threading;
using System.Threading.Tasks;
using Me20.Identity.QueryMessages;
using Me20.Identity.QueryResultMessages;

namespace Me20.Core.Contents
{
    public class GetContentDetailsQuery : IQuery<ContentEntity>
    {
        public Uri Uri {get;set;}

        //TODO: Exctract to separate methods
        public async Task<HttpResult<ContentEntity>> ExecuteAsync(string userName, CancellationToken ct)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return HttpResult<ContentEntity>.CreateErrorResult(401);

                if (Uri == null)
                    throw new ArgumentException("uri parameter in GetContentDetailsQuery should not be null");

                var contentDetailsQueryTask = ActorModel.ContentManagerActorRef.Ask(new GetContentDetailsQueryMessage(userName, Uri), ct);
                var userDetailsQueryTask = ActorModel.UsersManagerActorRef.Ask(new GetUserContentDetailsQueryMessage(userName, Uri), ct);

                var contentDetailsQueryResult = await contentDetailsQueryTask as GetContentDetailsQueryResultMessage;
                var usersContentDetailsQueryResult = await userDetailsQueryTask as GetUserContentDetailsQueryResultMessage;

                var result = new ContentEntity()
                {
                    Url = Uri.ToString()
                };
                if (contentDetailsQueryResult != null)
                {
                    result.AverageRating = contentDetailsQueryResult.AverageRating;
                    result.Rating = contentDetailsQueryResult.Rating;
                    result.Tags = contentDetailsQueryResult.Tags.Select(t => new TagDTO(t, false)).ToList();
                }
                if (usersContentDetailsQueryResult != null)
                {
                    result.Rating = usersContentDetailsQueryResult.Rating;

                    var userTags = usersContentDetailsQueryResult.Tags.Select(t => new TagDTO(t, true));

                    if (result.Tags.IsNullOrEmpty())
                        result.Tags = userTags.ToList();
                    else
                        result.Tags = result.Tags.Concat(userTags).GroupBy(t => t.TagName, StringComparer.OrdinalIgnoreCase)
                                     .Select(gTags => new TagDTO(gTags.Key, gTags.Any(t => t.TaggedByUser)))
                                     .ToList();
                }
                if (result != null)
                    return new HttpResult<ContentEntity>(result, 200);
                else
                    HttpResult<ContentEntity>.CreateErrorResult(404);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{nameof(GetContentDetailsQuery)} exception");
            }
            return HttpResult<ContentEntity>.CreateErrorResult(500);
        }
    }
}
