using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using Me20.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Me20.Identity.Actors
{
    public class UsersListActor : ReceivePersistentActorBase
    {
        public override string PersistenceId => nameof(UsersListActor).ToMD5();

        private HashSet<string> UserNames {get; set;}

        public UsersListActor() : base()
        {
            UserNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            Recover<SnapshotOffer>(offer => UserNames = (HashSet<string>)offer.Snapshot);

            Command<string>(userName => 
            {
                if (UserNames.Add(userName))
                    Persist(userName, un => HandleSnapshoting(UserNames));
            });
            Recover<string>(userName => UserNames.Add(userName));
        }

        public static Props Props => Props.Create(() => new UsersListActor());

    }
}
