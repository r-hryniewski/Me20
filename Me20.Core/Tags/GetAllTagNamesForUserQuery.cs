using System.Collections.Generic;
using Me20.Common.Interfaces;
using Akka.Actor;
using Me20.Identity.QueryMessages;
using System.Linq;
using Me20.Identity.QueryResultMessages;
using System;

namespace Me20.Core.Tags
{
    public class GetAllTagNamesForUserQuery : IQuery<TagEntity>
    {
        private readonly string userName;
        public GetAllTagNamesForUserQuery(string userName)
        {
            this.userName = userName;
        }
        public IEnumerable<TagEntity> Execute(IEnquire<TagEntity> enquirer)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("userName parameter in GetAllTagNamesForUserQuery should not be null or empty");

            var result = ActorModel.UsersManagerActorRef.Ask(new GetAllTagNamesForUserQueryMessage(userName), enquirer.AcceptableTimeout).Result as GetAllTagNamesForUserQueryResultMessage;

            return result == null ?
                Enumerable.Empty<TagEntity>() :
                result.TagNames.Select(tn => new TagEntity { TagName = tn });


        }
    }
}
