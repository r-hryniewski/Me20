using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Interfaces;
using Me20.Content.QueryMessages;
using System;

namespace Me20.Content.Actors
{
    public class TagsManagerActor : ReceiveActorBase
    {
        private readonly IActorRef tagsListActor;

        public TagsManagerActor() : base()
        {
            tagsListActor = Context.ActorOf(TagsListActor.Props, Guid.NewGuid().ToString());

            Receive<SubscribeToTagCommand>(msg => 
            {
                CreateTagActorIfNotExists(msg.TagName).Forward(msg);
            });

            Receive<GetTagsListQueryMessage>(msg => tagsListActor.Forward(msg));

            Receive<IHaveContentTag>(msg => CreateTagActorIfNotExists(msg.ContentTag).Forward(msg));

            Receive<IHaveContentTags>(msg =>
            {
                foreach (var tag in msg.ContentTags)
                    CreateTagActorIfNotExists(tag).Forward(msg);
            });
        }

        private IActorRef CreateTagActorIfNotExists(string tagName)
        {
            RegisterTag(tagName);

            var actorPath = ChildActorPathValidator(tagName);
            if (!Context.Child(actorPath).IsNobody())
                return Context.Child(actorPath);

            else
                return Context.ActorOf(TagActor.Props(tagName), actorPath);
        }

        private void RegisterTag(string tagName) => tagsListActor.Tell(tagName);

        public static Props Props => Props.Create(() => new TagsManagerActor());
    }
}
