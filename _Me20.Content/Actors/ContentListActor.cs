using System;
using Akka.Actor;
using Me20.Common.Abstracts;
using System.Collections.Generic;
using Akka.Persistence;
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;
using Me20.Common.Extensions;
using Me20.Common.Comparers;

namespace Me20.Content.Actors
{
    public class ContentListActor : ReceivePersistentActorBase
    {
        public override string PersistenceId => nameof(ContentListActor).ToMD5();
        private HashSet<Uri> ContentList { get; set; }

        public ContentListActor() : base()
        {
            ContentList = new HashSet<Uri>(new SchemalessMD5UriComparer());

            Recover<SnapshotOffer>(offer => ContentList = (HashSet<Uri>)offer.Snapshot);

            Command<Uri>(uri => 
            {
                if (ContentList.Add(uri))
                    Persist(uri, t => HandleSnapshoting(ContentList));
            });
            Recover<Uri>(uri => ContentList.Add(uri));
        }

        public static Props Props => Props.Create(() => new ContentListActor());
    }
}
