using Akka.Actor;
using Me20.Common.Comparers;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using Me20.Content.QueryResultMessages;
using Me20.Core.DTO;
using Me20.Identity.QueryMessages;
using Me20.Identity.QueryResultMessages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Me20.Core.Contents
{
    public class GetTaggedContentQuery : IQuery<ContentEntity>
    {
        private readonly IEnumerable<string> tagNames;
        public GetTaggedContentQuery(IEnumerable<string> tagNames)
        {
            this.tagNames = tagNames;
        }
        public IEnumerable<ContentEntity> Execute(IEnquire<ContentEntity> enquirer)
        {
            var tasks = new List<Task>();
            var results = new ConcurrentBag<ContentEntity>();

            var taggedContentsHashset = new HashSet<Uri>(new SchemalessMD5UriComparer());
            var taggedContentsQueue = new ConcurrentQueue<Uri>();

            var taggedContentsTasks = tagNames.Select(tag =>
                ActorModel.TagsManagerActorRef.Ask("TODO: TaggedContentQuery").ContinueWith(t =>
                /*TODO: t.Result as QueryResult*/taggedContentsQueue.Enqueue(new Uri("TODO")))
            ).ToArray();

            Task.WaitAny(taggedContentsTasks);

            while (taggedContentsQueue.TryDequeue(out Uri ContentUri) && results.Count <= 10)
            {
                tasks.Add(ActorModel.ContentManagerActorRef.Ask(new GetContentDetailsQuery(string.Empty, ContentUri)).ContinueWith(t =>
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
                ));
            }

            return results.IsNullOrEmpty() ?
                Enumerable.Empty<ContentEntity>() :
                results;

        }
    }
}
