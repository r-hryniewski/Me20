using Akka.Actor;
using Me20.Common.Messages;
using System;
using System.Linq;

namespace Me20.Core.Actors
{
    //TODO: Make it persistent actor
    public class UserActor : ReceiveActor
    {
        private UserActorState actorState { get; set; }
        public UserActor()
        {
            actorState = new UserActorState();
            //TODO: Persist/Receive + User repo?
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage msg)
        {
            if (actorState.HasChanged(msg))
                actorState.Update(msg);
        }

        public static Props Props => Props.Create(() => new UserActor());

        
        private class UserActorState
        {
            //TODO: Extract those properties to some abstract class
            public string UserName { get; private set; } = string.Empty;

            public string Id { get; private set; } = string.Empty;

            public string FullName { get; private set; } = string.Empty;
            public string FirstName { get; private set; } = string.Empty;
            public string LastName { get; private set; } = string.Empty;

            public string Email { get; private set; } = string.Empty;

            public string Gender { get; private set; } = string.Empty;

            public string AuthenticationType { get; private set; } = string.Empty;

            public UserActorState() { }

            //TODO: Refactor this
            public bool HasChanged(UserLoggedInMessage msg)
            {
                return (new bool[]
                {
                    this.UserName.Equals(msg.UserName, StringComparison.OrdinalIgnoreCase),
                    this.Id.Equals(msg.Id, StringComparison.OrdinalIgnoreCase),
                    this.FullName.Equals(msg.FullName, StringComparison.OrdinalIgnoreCase),
                    this.FirstName.Equals(msg.FirstName, StringComparison.OrdinalIgnoreCase),
                    this.LastName.Equals(msg.LastName, StringComparison.OrdinalIgnoreCase),
                    this.Email.Equals(msg.Email, StringComparison.OrdinalIgnoreCase),
                    this.Gender.Equals(msg.Gender, StringComparison.OrdinalIgnoreCase),
                    this.AuthenticationType.Equals(msg.AuthenticationType, StringComparison.OrdinalIgnoreCase)
                })
                .Any(x => !x);
            }

            //TODO: Refactor this
            public void Update(UserLoggedInMessage msg)
            {
                this.UserName = msg.UserName;
                this.Id = msg.Id;
                this.FullName = msg.FullName;
                this.FirstName = msg.FirstName;
                this.LastName = msg.LastName;
                this.Email = msg.Email;
                this.Gender = msg.Gender;
                this.AuthenticationType = msg.AuthenticationType;
            }
        }
    }
}
