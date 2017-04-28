using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using System.Linq;

namespace Me20.Core.Contents
{
    public class TagContentDispatcher : DispatcherBase<ContentEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "TagContent";

        public TagContentDispatcher() : base()
        { }

        public override void Dispatch(ContentEntity item, string userName)
        {
            var command = new TagContentCommand(item.Uri, userName, item.Tags.Select(t => t.TagName));
            ActorModel.ContentManagerActorRef.Tell(command);
            ActorModel.TagsManagerActorRef.Tell(command);
            ActorModel.UsersManagerActorRef.Tell(command);
        }
    }
}
