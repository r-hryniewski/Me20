using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Content.Events;
using System;
using System.Collections.Generic;

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

            private Dictionary<string, byte> ratings;
            internal IReadOnlyDictionary<string, byte> Ratings => ratings;
            
            internal ContentActorState(Uri uri)
            {
                Uri = uri;
                ratings = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
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
        }
    }
}
