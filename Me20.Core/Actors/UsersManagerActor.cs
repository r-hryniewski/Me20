using Akka.Actor;
using Me20.Core.Messages;
using System;
using System.Collections.Generic;

namespace Me20.Core.Actors
{
    public class UsersManagerActor : ReceiveActor
    {
        private Dictionary<string, IActorRef> usersDictionary; // UserName/ActorRef

        public UsersManagerActor()
        {
            usersDictionary = new Dictionary<string, IActorRef>(StringComparer.OrdinalIgnoreCase);

            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage message)
        {
            if (usersDictionary.ContainsKey(message.UserName))
                usersDictionary[message.UserName].Tell(message);

            else
            {
                
                //TODO: Create UserActor and pass him it's data
            }
        }

        public static Props Props => Props.Create(() => new UsersManagerActor());
    }
}
