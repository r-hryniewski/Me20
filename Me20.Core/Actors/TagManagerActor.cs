using Akka.Actor;
using Me20.Common.Helpers;
using Me20.Common.Messages;
using Me20.Content.Actors;

namespace Me20.Core.Actors
{
    public class TagManagerActor : ReceiveActor
    {
        public TagManagerActor()
        {
            Receive<TagAddedMessage>(msg => HandleTagAddedMessage(msg));
        }

        private void HandleTagAddedMessage(TagAddedMessage msg)
        {
            Context.ActorSelection(ActorPathsHelper.BuildAbsoluteActorPath(ActorPathsHelper.UsersManagerActorName)).Tell(msg);

            CreateTagActorIfNotExists(msg.TagName);
        }

        private void CreateTagActorIfNotExists(string tagName)
        {
            if (!Context.Child(tagName).IsNobody())
                Context.Child(tagName).Tell(tagName);

            else
            {
                var newUserActor = Context.ActorOf(TagActor.Props, tagName);
                newUserActor.Tell(tagName);
            }
        }

        public static Props Props => Props.Create(() => new TagManagerActor());
    }
}
