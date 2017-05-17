using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.Collections.Generic;
using Me20.Bot.Dialogs;
using Autofac;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System.Threading;

namespace Me20.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity, CancellationToken ct)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => Chain.From(() => new LUISDialog()));
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private IEnumerable<Activity> HandleSystemMessage(Activity message)
        {
            var replies = new List<Activity>();
            if (message.Type == ActivityTypes.DeleteUserData)
            {
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                //if (message.MembersAdded.Any(ma => ma.Id == message.From.Id))
                //{
                //    replies.Add(message.CreateReply($"Hi there {message.From.Name}! I'm a bot created as part of Me 2.0 project, you can read more about me at https://hryniewski.net!")); replies.Add(message.CreateReply($"For now you can just enter tag name (ie. c#, dotnet) of topic you're interested in and I'll give you some links about it."));
                //    replies.Add(message.CreateReply($"If you're participating in Get Noticed 2017 contest (like my very smart and godlike creator) you can enter tag 'DSP' which is where Me 2.0 stores links to all posts contestants written."));
                //}
                //// Handle conversation state changes, like members being added and removed
                //// Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                //// Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.Typing)
            {
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return replies;
        }
    }
}