using Akka.Actor;
using Me20.Common.Messages;
using Me20.Core.Abstracts;

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
            var tagSubscribedMessage = new TagSubscribedMessage(userName, item.TagName);

            ActorModel.TagsManagerActorRef.Tell(tagSubscribedMessage);
            ActorModel.UsersManagerActorRef.Tell(tagSubscribedMessage);
        }
    }
}