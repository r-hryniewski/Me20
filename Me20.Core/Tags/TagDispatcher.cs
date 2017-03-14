using Akka.Actor;
using Me20.Common.DTO;
using Me20.Common.Messages;
using Me20.Core.Interfaces;
using System.Net;

namespace Me20.Core.Tags
{
    public class TagDispatcher : IDispatch<Tag>
    {
        public TagDispatcher()
        {
        }

        //TODO: Tag As param after choosing frontend framework and implementing posts and model bindings
        public HttpResult<Tag> Subsribe(dynamic parameters, string userName)
        {
            var tag = new Tag(parameters.tagName);

            var tagSubscribedMessage = new TagSubscribedMessage(userName, tag.TagName);

            ActorModel.TagsManagerActorRef.Tell(tagSubscribedMessage);
            ActorModel.UsersManagerActorRef.Tell(tagSubscribedMessage);

            return new HttpResult<Tag>(tag, HttpStatusCode.OK);
        }
    }
}