using Akka.Actor;
using Me20.Common.Messages;
using Me20.Core.Abstracts;

namespace Me20.Core.Tags
{
    public class CreateTagIfNotExistsDispatcher : DispatcherBase<Tag>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "CreateTagIfNotExists";

        public CreateTagIfNotExistsDispatcher() : base()
        {}

        public override void Dispatch(Tag item, string userName)
        {
            ActorModel.TagsManagerActorRef.Tell(new CreateTagIfNotExistsMessage(item.TagName));
        }
    }
}