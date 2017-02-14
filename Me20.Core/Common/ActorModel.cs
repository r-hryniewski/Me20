﻿using Akka.Actor;
using Me20.Core.Actors;

namespace Me20.Core
{
    public static class ActorModel
    {
        public static ActorSystem MainActorSystem { get; set; }

        public static IActorRef UsersManagerActorRef { get; set; }

        public static void StartActorSystem()
        {
            MainActorSystem = ActorSystem.Create("MainSystem");

            UsersManagerActorRef = MainActorSystem.ActorOf(Props.Create(() => new UsersManagerActor()), "UsersManager");
        }
    }
}