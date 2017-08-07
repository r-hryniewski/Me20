using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Me20.ActorModel.ActorPathsHelper;

namespace Me20.ActorModel
{
    public class ActorModel
    {
        public static ActorSystem MainActorSystem { get; set; }

        public static IActorRef UsersManagerActorRef { get; set; }

        public static void StartActorSystem()
        {
            MainActorSystem = ActorSystem.Create(ActorSystemName);

            //UsersManagerActorRef = MainActorSystem.ActorOf(UsersManagerActor.Props, UsersManagerActorName);
        }

        public static ActorSelection GetUserActorSelection(string userName) => MainActorSystem.ActorSelection($"{UsersManagerActorRef.Path.ToStringWithAddress()}/{userName}");
    }
}
