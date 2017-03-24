﻿using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Identity.Abstracts;
using Me20.Identity.Messages;
using System;
using System.Collections.Generic;

namespace Me20.Identity.Actors
{
    //TODO: Make it persistent actor
    public class UserActor : ReceiveActorBase
    {
        private UserActorState ActorState { get; set; }
        public UserActor(string authenthicationType, string id) : base()
        {
            ActorState = new UserActorState(authenthicationType, id);
            //TODO: Persist/Receive + User repo?
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));

            Receive<AddSubscribedTagCommand>(msg => HandleAddSubscribedTagCommand(msg));
        }

        private void HandleAddSubscribedTagCommand(AddSubscribedTagCommand msg)
        {
            ActorState.AddSubscribedTag(msg.TagName);
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage msg)
        {
            ActorState.RefreshLastLoggedIn();
        }

        public static Props Props(string authenthicationType, string id)
        {
            return Akka.Actor.Props.Create<UserActor>(() => new UserActor(authenthicationType, id));
        }

        private sealed class UserActorState : UserDataBase
        {
            internal DateTime LastLoggedIn { get; private set; }
            private readonly HashSet<string> subscribedTags;
            //NYI
            //internal IReadOnlyCollection<string> SubscribedTags => subscribedTags;

            internal UserActorState(string authenthicationType, string id) : base(authenthicationType, id)
            {
                subscribedTags = new HashSet<string>();
                RefreshLastLoggedIn();
            }

            internal void AddSubscribedTag(string tagName)
            {
                subscribedTags.Add(tagName);
            }

            internal void RefreshLastLoggedIn() => LastLoggedIn = DateTime.UtcNow;

            //TODO: Refactor this
            //Not used at the moment
            //internal bool HasChanged(UserLoggedInMessage msg)
            //{
            //    return (new bool[]
            //    {
            //        this.UserName.Equals(msg.UserName, StringComparison.OrdinalIgnoreCase),
            //        //this.Id.Equals(msg.Id, StringComparison.OrdinalIgnoreCase),
            //        //this.FullName.Equals(msg.FullName, StringComparison.OrdinalIgnoreCase),
            //        //this.FirstName.Equals(msg.FirstName, StringComparison.OrdinalIgnoreCase),
            //        //this.LastName.Equals(msg.LastName, StringComparison.OrdinalIgnoreCase),
            //        //this.Email.Equals(msg.Email, StringComparison.OrdinalIgnoreCase),
            //        //this.Gender.Equals(msg.Gender, StringComparison.OrdinalIgnoreCase),
            //        //this.AuthenticationType.Equals(msg.AuthenticationType, StringComparison.OrdinalIgnoreCase)
            //    })
            //    .Any(x => !x);
            //}

            //TODO: Change type
            //Not used at the moment
            //internal void Update(UserLoggedInMessage msg)
            //{
            //    //TODO: Not used atm

            //    //this.Id = msg.Id;
            //    //this.FullName = msg.FullName;
            //    //this.FirstName = msg.FirstName;
            //    //this.LastName = msg.LastName;
            //    //this.Email = msg.Email;
            //    //this.Gender = msg.Gender;
            //    //this.AuthenticationType = msg.AuthenticationType;
            //}
        }
    }
}
