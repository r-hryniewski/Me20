﻿using Akka.Actor;
using Me20.Contracts.Actors;
using Me20.IdentityActors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Me20.ActorModel.ActorPathsHelper;

namespace Me20.ActorModel
{
    public class ActorSystemContainer : IKnowActor<UsersManagerActor>
    {
        public ActorSystem System { get; private set; }

        public IActorRef UsersManagerActorRef { get; private set; }
        IActorRef IKnowActor<UsersManagerActor>.Ref => UsersManagerActorRef;

        public ActorSystemContainer()
        {
            System = ActorSystem.Create(ActorSystemName);

            UsersManagerActorRef = System.ActorOf(UsersManagerActor.Props, UsersManagerActorName);
        }
    }
}
