using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;

namespace Me20.Core.Tags
{
    public class TagSubscribedDispatcher : DispatcherBase<TagEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "TagSubscribed";

        public TagSubscribedDispatcher() : base()
        { }

        public override void Dispatch(TagEntity item, string userName)
        {
            //TODO: Go through user manager for that
            var command = new SubscribeToTagCommand(userName, item.TagName);
            if (item.TagName.Length <= 25)
            {
                ActorModel.UsersManagerActorRef.Tell(command);
                ActorModel.TagsManagerActorRef.Tell(command);
            }

        }
    }
}