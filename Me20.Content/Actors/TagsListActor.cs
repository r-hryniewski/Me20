using System;
using Akka.Actor;
using Me20.Common.Abstracts;
using System.Collections.Generic;
using Akka.Persistence;
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;
using Me20.Common.Extensions;

namespace Me20.Content.Actors
{
    public class TagsListActor : ReceivePersistentActorBase
    {
        public override string PersistenceId => nameof(TagsListActor).ToMD5();
        private HashSet<string> TagsList { get; set; }

        public TagsListActor() : base()
        {
            TagsList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            Command<string>(tagName => 
            {
                if (TagsList.Add(tagName))
                    Persist(tagName, t => HandleSnapshoting(TagsList));
            });
            Recover<string>(tagName => TagsList.Add(tagName));

            Command<GetTagsListQueryMessage>(msg => Sender.Tell(new GetTagsListQueryResultMessage(TagsList)));

            Recover<SnapshotOffer>(offer => TagsList = (HashSet<string>)offer.Snapshot);
        }

        public static Props Props => Props.Create(() => new TagsListActor());

    }
}
