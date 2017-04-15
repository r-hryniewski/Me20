using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;

namespace Me20.Core.Contents
{
    public class RateContentDispatcher : DispatcherBase<ContentEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "RateContent";

        public RateContentDispatcher() : base()
        { }

        public override void Dispatch(ContentEntity item, string userName)
        {
            var command = new AddContentCommand(item.Uri, userName, item.Tags);
            ActorModel.UsersManagerActorRef.Tell(command);
            ActorModel.ContentManagerActorRef.Tell(command);
        }
    }
}
