using Me20.Common.Helpers;
using Me20.Common.Commands;
using Me20.Common.Abstracts;

namespace Me20.Core.Tags
{
    public class TagSubscribedDispatcher : DispatcherBase<Tag>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "TagSubscribed";

        public TagSubscribedDispatcher() : base()
        {}

        public override void Dispatch(Tag item, string userName)
        {
            ActorModel.MainActorSystem.ActorSelection(ActorPathsHelper.BuildAbsoluteActorPath(ActorPathsHelper.UsersManagerActorName, userName)).Tell(new AddSubscribedTagCommand(item.TagName));
            ActorModel.MainActorSystem.ActorSelection(ActorPathsHelper.BuildAbsoluteActorPath(ActorPathsHelper.TagsManagerActorName, item.TagName)).Tell(new AddSubscriberCommand(userName));
        }
    }
}