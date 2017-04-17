using Akka.Actor;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using Me20.Core.DTO;
using Me20.Identity.QueryMessages;
using Me20.Identity.QueryResultMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Core.Contents
{
    public class GetUserContentDetailsQuery : IQuery<ContentEntity>
    {
        private readonly string userName;
        private readonly Uri uri;
        public GetUserContentDetailsQuery(string userName, Uri uri)
        {
            this.userName = userName;
            this.uri = uri;
        }
        public IEnumerable<ContentEntity> Execute(IEnquire<ContentEntity> enquirer)
        {
            if (uri == null)
                throw new ArgumentException("uri parameter in GetUserContentDetailsQuery should not be null");

            if (string.IsNullOrEmpty(userName))
                    throw new ArgumentException("userName parameter in GetUserContentDetailsQuery should not be null or empty");

            var result = ActorModel.UsersManagerActorRef.Ask(new GetUserContentDetailsQueryMessage(userName, uri), enquirer.AcceptableTimeout).Result as GetUserContentDetailsQueryResultMessage;

            if (result == null)
                return Enumerable.Empty<ContentEntity>();
            else
                return new ContentEntity()
                {
                    Url = result.Uri.ToString(),
                    AverageRating = 0,
                    Rating = result.Rating,
                    Tags = result.Tags.Select(t => new TagDTO(t, true)).ToList()
                }.AsEnumerable();
        }
    }
}
