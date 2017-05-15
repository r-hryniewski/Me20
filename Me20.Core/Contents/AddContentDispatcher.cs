using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Extensions;
using System.Linq;

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
            var command = new AddContentCommand(item.Uri, item.Title, userName, item.Tags.Select(t => t.TagName));
            ActorModel.ContentManagerActorRef.Tell(command);

            if (!command.ContentTags.IsNullOrEmpty())
                ActorModel.TagsManagerActorRef.Tell(command);

            if (!string.IsNullOrEmpty(command.UserName))
                ActorModel.UsersManagerActorRef.Tell(command);
        }
    }
}
