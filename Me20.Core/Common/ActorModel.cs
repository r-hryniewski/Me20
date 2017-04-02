using Akka.Actor;
using Me20.Content.Actors;
using Me20.Identity.Actors;
using static Me20.Common.Helpers.ActorPathsHelper;

namespace Me20.Core
{
    public static class ActorModel
    {
        public static ActorSystem MainActorSystem { get; set; }

        public static IActorRef UsersManagerActorRef { get; set; }
        public static IActorRef TagsManagerActorRef { get; set; }
        public static IActorRef ContentManagerActorRef { get; set; }
        //TODO: Anonymous UserActor?

        public static void StartActorSystem()
        {
            MainActorSystem = ActorSystem.Create(ActorSystemName);

            UsersManagerActorRef = MainActorSystem.ActorOf(UsersManagerActor.Props, UsersManagerActorName);
            TagsManagerActorRef = MainActorSystem.ActorOf(TagsManagerActor.Props, TagsManagerActorName);
            ContentManagerActorRef = MainActorSystem.ActorOf(ContentManagerActor.Props, ContentManagerActorName);
        }

        public static ActorSelection GetUserActorSelection(string userName) => MainActorSystem.ActorSelection($"{UsersManagerActorRef.Path.ToStringWithAddress()}/{userName}");


        //private static Config CreateConfiguration()
        //{
        //    return ConfigurationFactory.ParseString($@"
        //        akka.persistence {{
        //            journal {{
        //                plugin = ""akka.persistence.journal.sql-server""                
        //                sql-server {{
        //                    class = ""Akka.Persistence.SqlServer.Journal.SqlServerJournal, Akka.Persistence.SqlServer""
        //                    plugin-dispatcher = ""akka.actor.default-dispatcher""

        //                    # connection string used for database access
        //                    connection-string-name = ""Me20DbConnectionString""
        //                    # can alternativly specify: connection-string-name

        //                    # default SQL timeout
        //                    connection-timeout = 30s

        //                    # SQL server schema name
        //                    schema-name = dbo

        //                    # persistent journal table name
        //                    table-name = EventJournal

        //                    # initialize journal table automatically
        //                    auto-initialize = on

        //                    timestamp-provider = ""Akka.Persistence.Sql.Common.Journal.DefaultTimestampProvider, Akka.Persistence.Sql.Common""
        //                    metadata-table-name = Metadata
        //                }}
        //            }}

        //        snapshot-store {{
        //                plugin = ""akka.persistence.snapshot-store.sql-server""
        //                sql-server {{
        //                    class = ""Akka.Persistence.SqlServer.Snapshot.SqlServerSnapshotStore, Akka.Persistence.SqlServer""
        //                    plugin-dispatcher = ""akka.actor.default-dispatcher""
        //                    table-name = SnapshotStore
        //                    schema-name = dbo
        //                    auto-initialize = on
        //                    connection-string-name = ""Me20DbConnectionString""
        //                }}
        //            }} 
        //  }}");
        //}
    }
}