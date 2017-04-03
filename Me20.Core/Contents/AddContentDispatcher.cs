using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;

namespace Me20.Core.Contents
{
    public class AddContentDispatcher : DispatcherBase<ContentEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "AddContent";

        public AddContentDispatcher() : base()
        { }

        public override void Dispatch(ContentEntity item, string userName)
        {
            ActorModel.UsersManagerActorRef.Tell(new AddContentCommand(item.Uri, userName, item.Tags));
        }
    }
}
