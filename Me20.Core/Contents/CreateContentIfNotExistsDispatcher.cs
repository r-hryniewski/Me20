using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Messages;

namespace Me20.Core.Contents
{
    public class CreateContentIfNotExistsDispatcher : DispatcherBase<Content>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "CreateContentIfNotExists";

        public CreateContentIfNotExistsDispatcher() : base()
        {}

        public override void Dispatch(Content item, string userName)
        {
            ActorModel.ContentManagerActorRef.Tell(new CreateContentIfNotExistsMessage(item.Uri));
        }
    }
}