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
            EnsureValidInput(item);
            var command = new RateContentCommand(item.Uri, userName, item.Rating);
            ActorModel.UsersManagerActorRef.Tell(command);
            ActorModel.ContentManagerActorRef.Tell(command);
        }

        private void EnsureValidInput(ContentEntity item)
        {
            if (item.Rating < 1)
                item.Rating = 1;
            else if (item.Rating > 5)
                item.Rating = 5;
        }
    }
}
