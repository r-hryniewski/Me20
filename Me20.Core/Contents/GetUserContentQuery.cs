using Akka.Actor;
using Me20.Common.Interfaces;
using Me20.Identity.QueryMessages;
using Me20.Identity.QueryResultMessages;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Core.Contents
{
    public class GetUserContentQuery : IQuery<ContentEntity>
    {
        private readonly string userName;
        public GetUserContentQuery(string userName)
        {
            this.userName = userName;
        }
        public IEnumerable<ContentEntity> Execute(IEnquire<ContentEntity> enquirer)
        {
            if (string.IsNullOrEmpty(userName))
                return Enumerable.Empty<ContentEntity>();

            var result = ActorModel.UsersManagerActorRef.Ask(new GetUserContentQueryMessage(userName), enquirer.AcceptableTimeout).Result as GetUserContentQueryResultMessage;

            return result == null ?
                Enumerable.Empty<ContentEntity>() :
                result.ContentWithTags.Select(cwt =>
                    new ContentEntity
                    {
                        Url = cwt.Key.ToString(),
                        Tags = cwt.Value
                    }
                );


        }
    }
}
