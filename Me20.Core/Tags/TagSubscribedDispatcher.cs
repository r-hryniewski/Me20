using Akka.Actor;
using Me20.Common.Helpers;
using Me20.Common.Messages;
using Me20.Core.Abstracts;
using Me20.Common.Commands;

namespace Me20.Core.Tags
{
    public class TagSubscribedDispatcher : DispatcherBase<Tag>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "TagSubscribed";

        public TagSubscribedDispatcher() : base()
        {}

        public override void Dispatch(Tag item, string userName)
        {
            //var message = new TagSubscribedMessage(userName, item.TagName);
            ActorModel.MainActorSystem.ActorSelection(ActorPathsHelper.BuildAbsoluteActorPath(ActorPathsHelper.UsersManagerActorName, userName)).Tell(new AddSubscribedTagCommand(item.TagName));

            ActorModel.MainActorSystem.ActorSelection(ActorPathsHelper.BuildAbsoluteActorPath(ActorPathsHelper.TagsManagerActorName, item.TagName)).Tell(new AddSubscriberCommand(userName));

            //ActorModel.TagsManagerActorRef.Tell(message);
            //ActorModel.UsersManagerActorRef.Tell(message);
        }
    }
}