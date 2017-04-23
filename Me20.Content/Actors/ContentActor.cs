using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Extensions;
using Me20.Content.Events.Contents;
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Content.Actors
{
    public class ContentActor : ReceivePersistentActorBase
    {
        private ContentActorState ActorState { get; set; }
        
        public override string PersistenceId => $"content-{ActorState.Uri}";

        public ContentActor(Uri uri) : base()
        {
            //validate url
            ActorState = new ContentActorState(uri);

            Recover<SnapshotOffer>(offer => ActorState = (ContentActorState)offer.Snapshot);

            Command<RateContentCommand>(cmd => HandleRateContentCommand(cmd));
            Recover<ContentRatedByEvent>(ev => ActorState.Rate(ev.UserName, ev.Rating));

            Command<AddContentCommand>(cmd => HandleAddContentCommand(cmd));
            Command<TagContentCommand>(cmd => HandleTagContentCommand(cmd));
            Recover<ContentTaggedWithEvent>(ev => ActorState.UpdateTags(ev.Tags));

            Command<GetContentDetailsQueryMessage>(msg => Sender.Tell(new GetContentDetailsQueryResultMessage(ActorState.Uri, ActorState.AverageRating, ActorState.Tags, ActorState.Ratings.ContainsKey(msg.UserName) ? ActorState.Ratings[msg.UserName] : (byte)0)));
        }

        private void HandleTagContentCommand(TagContentCommand cmd) => TagContentWith(cmd.ContentTags);

        private void HandleAddContentCommand(AddContentCommand cmd) => TagContentWith(cmd.ContentTags);

        private void TagContentWith(IEnumerable<string> tagNames)
        {
            if (!tagNames.IsNullOrEmpty())
            {
                var @event = new ContentTaggedWithEvent(tagNames);
                if (ActorState.UpdateTags(@event.Tags))
                    Persist(@event, ev => HandleSnapshoting(ActorState));
            }
        }

        private void HandleRateContentCommand(RateContentCommand cmd)
        {
            var @event = new ContentRatedByEvent(cmd.UserName, cmd.Rating);
            Persist(@event, ev =>
            {
                ActorState.Rate(ev.UserName, ev.Rating);
                HandleSnapshoting(ActorState);
            });
        }

        public static Props Props(Uri uri) => Akka.Actor.Props.Create(() => new ContentActor(uri));

        private sealed class ContentActorState
        {
            internal Uri Uri { get; private set; }

            private readonly Dictionary<string, byte> ratings;
            internal IReadOnlyDictionary<string, byte> Ratings => ratings;
            internal double AverageRating => Ratings.Values.Any() ? Ratings.Values.Select(r => (int)r).Average() : 0;
            internal readonly HashSet<string> Tags;

            internal ContentActorState(Uri uri)
            {
                Uri = uri;
                ratings = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                //TODO: Level
                //TODO: ReadedBy
                //TODO: TimeLastOpened
            }

            internal void Rate(string userName, byte rating)
            {
                if (ratings.ContainsKey(userName))
                    ratings[userName] = rating;
                else
                    ratings.Add(userName, rating);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="tags"></param>
            /// <returns>True if state has changed (any tags were added)</returns>
            internal bool UpdateTags(IEnumerable<string> tags) => Tags.AddRangeAny(tags);
        }
    }
}
