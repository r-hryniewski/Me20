using Akka.Actor;
using Me20.Common.Comparers;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;
using Me20.Core.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Me20.Common.DTO;
using System.Threading;

namespace Me20.Core.Contents
{
    //TODO: Rename
    public class GetTaggedContentQuery : IAnonymousQuery<IEnumerable<ContentEntity>>
    {
        public string[] Tags { get; set; }
        public int Count { get; set; }

        public GetTaggedContentQuery()
        {
            Count = 10;
        }
        public async Task<HttpResult<IEnumerable<ContentEntity>>> ExecuteAsync(CancellationToken ct)
        {
            try
            {
                if (Tags.IsNullOrEmpty())
                    return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(400);

                var results = new ConcurrentBag<ContentEntity>();
                var taggedContentsStack = new ConcurrentStack<Uri>();

                var taggedContentsTasks = Tags.Select(tag =>
                    ActorModel.TagsManagerActorRef.Ask(new GetTaggedContentQueryMessage(tag), ct).ContinueWith(t =>
                    {
                        if (t.Result is GetTaggedContentQueryResultMessage result && !result.Contents.IsNullOrEmpty())
                            taggedContentsStack.PushRange(result.Contents.ToArray());
                    })
                ).ToArray();

                Task.WaitAny(taggedContentsTasks);

                while (taggedContentsStack.TryPop(out Uri contentUri) && results.Count < Count)
                {
                    if (await ActorModel.ContentManagerActorRef.Ask(new GetContentDetailsQueryMessage(string.Empty, contentUri), ct) is GetContentDetailsQueryResultMessage queryResult)
                    {
                        results.Add(new ContentEntity()
                        {
                            Url = queryResult.Uri.ToString(),
                            AverageRating = queryResult.AverageRating,
                            Rating = queryResult.Rating,
                            Tags = queryResult.Tags.Select(tag => new TagDTO(tag, false)).ToList(),
                            Title = queryResult.Title
                        });
                    }
                }

                return new HttpResult<IEnumerable<ContentEntity>>(results.Any() ?
                    results.Take(Count).ToArray() :
                    Enumerable.Empty<ContentEntity>());
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{nameof(GetTaggedContentQuery)} exception");
            }
            return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(500);
        }
    }
}
