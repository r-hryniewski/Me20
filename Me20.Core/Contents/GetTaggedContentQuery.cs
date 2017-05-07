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

namespace Me20.Core.Contents
{
    //TODO: Rename
    public class GetTaggedContentQuery : IQuery<ContentEntity>
    {
        private readonly IEnumerable<string> tagNames;
        public int Count { get; set; } = 10;

        public GetTaggedContentQuery(IEnumerable<string> tagNames)
        {
            this.tagNames = tagNames;
        }

        //TODO: Refactor
        public IEnumerable<ContentEntity> Execute(IEnquire<ContentEntity> enquirer)
        {
            var results = new ConcurrentBag<ContentEntity>();
            var taggedContentsQueue = new ConcurrentQueue<Uri>();

            var taggedContentsTasks = tagNames.Select(tag =>
                ActorModel.TagsManagerActorRef.Ask(new GetTaggedContentQueryMessage(tag)).ContinueWith(t =>
                {
                    if (t.Result is GetTaggedContentQueryResultMessage result && !result.Contents.IsNullOrEmpty())
                        foreach (var content in result.Contents)
                            taggedContentsQueue.Enqueue(content);
                })
            ).ToArray();

            Task.WaitAny(taggedContentsTasks);

            while (taggedContentsQueue.TryDequeue(out Uri contentUri) && results.Count <= Count)
            {
                ActorModel.ContentManagerActorRef.Ask(new GetContentDetailsQueryMessage(string.Empty, contentUri)).ContinueWith(t =>
                    {
                        if (t.Result is GetContentDetailsQueryResultMessage queryResult)
                            results.Add(new ContentEntity()
                            {
                                Url = queryResult.Uri.ToString(),
                                AverageRating = queryResult.AverageRating,
                                Rating = queryResult.Rating,
                                Tags = queryResult.Tags.Select(tag => new TagDTO(tag, false)).ToList()
                            });
                    }
                );
            }

            return results.IsNullOrEmpty() ?
                Enumerable.Empty<ContentEntity>() :
                results.Take(10).ToArray();
        }
    }
}
