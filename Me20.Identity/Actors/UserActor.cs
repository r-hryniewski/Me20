using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Core.Actors
{
    //TODO: Make it persistent actor
    public class UserActor : ReceiveActor
    {
        public UserActor()
        {
            //TODO: User repo? + Persist/Receive
            //TODO: User/ActorRef map?
            //TODO: 
            
        }


        public static Props Props => Props.Create(() => new UserActor());
    }
}
