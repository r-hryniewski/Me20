﻿using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;

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
            //TODO: Go through user manager for that
            var command = new SubscribeToTagCommand(userName, item.TagName);

            ActorModel.UsersManagerActorRef.Tell(command);
            ActorModel.TagsManagerActorRef.Tell(command);
        }
    }
}