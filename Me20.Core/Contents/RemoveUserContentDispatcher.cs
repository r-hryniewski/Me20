using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using System.Linq;

namespace Me20.Core.Contents
{
    public class RemoveUserContentDispatcher : DispatcherBase<ContentEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "RemoveUserContent";

        public RemoveUserContentDispatcher() : base()
        { }

        public override void Dispatch(ContentEntity item, string userName)
        {
            ActorModel.UsersManagerActorRef.Tell(new RemoveUserContentCommand(item.Uri, userName));
        }
    }
}
