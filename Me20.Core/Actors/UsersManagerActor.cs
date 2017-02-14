using Akka.Actor;
using Me20.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Core.Actors
{
    public class UsersManagerActor : ReceiveActor
    {
        public UsersManagerActor()
        {
            //TODO: User repo? + Persist/Receive
            //TODO: User/ActorRef map?
            //TODO: 
            Receive<UserLoggedInMessage>(msg => 
            {
                //TODO:
                Console.WriteLine("Msg Received Placeholder");
            });
            
        }
    }
}
