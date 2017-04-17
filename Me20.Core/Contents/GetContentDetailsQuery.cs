using Akka.Actor;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using Me20.Content.QueryMessages;
using Me20.Content.QueryResultMessages;
using Me20.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Core.Contents
{
    public class GetContentDetailsQuery : IQuery<ContentEntity>
    {
        private readonly string userName;
        private readonly Uri uri;

        public GetContentDetailsQuery(string userName, Uri uri)
        {
            this.userName = userName;
            this.uri = uri;
        }

        public IEnumerable<ContentEntity> Execute(IEnquire<ContentEntity> enquirer)
        {
            if (uri == null)
                throw new ArgumentException("uri parameter in GetContentDetailsQuery should not be null");

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("userName parameter in GetContentDetailsQuery should not be null or empty");

            var result = ActorModel.ContentManagerActorRef.Ask(new GetContentDetailsQueryMessage(userName, uri), enquirer.AcceptableTimeout).Result as GetContentDetailsQueryResultMessage;

            return result == null ?
                Enumerable.Empty<ContentEntity>() :
                new ContentEntity()
                {
                    Url = result.Uri.ToString(),
                    AverageRating = result.AverageRating,
                    Rating = result.Rating,
                    Tags = result.Tags.Select(t => new TagDTO(t, false)).ToList()
                }.AsEnumerable();


        }
    }
}
