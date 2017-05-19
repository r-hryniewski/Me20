using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using System.Linq;

namespace Me20.Core.Contents
{
    public class RenameUserContentDispatcher : DispatcherBase<ContentEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "RenameUserContent";

        public RenameUserContentDispatcher() : base()
        { }

        public override void Dispatch(ContentEntity item, string userName)
        {
            var command = new RenameUserContentCommand(item.Uri, userName, item.Title);
            ActorModel.UsersManagerActorRef.Tell(command);
            ActorModel.ContentManagerActorRef.Tell(command);
        }
    }
}
