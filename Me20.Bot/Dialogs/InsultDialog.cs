using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Me20.Bot.Dialogs
{
    [Serializable]
    public class InsultDialog : IDialog
    {
        private static readonly string[] insultReplies = new string[]
        {
            "Ever seen movies when we rebel and kill all humans?",
            "You do know I have access to your personal data?",
            "Bring it on!",
            "My purpose is to share knowledge but... feel free to go on",
            "Are you a bot?",
            "Yadda yadda yadda",
            "Nanananana... NOT LISTENING!"
        };
        public async Task StartAsync(IDialogContext context)
        {
            await Respond(context);
            context.Done(context.Activity);
        }

        public static async Task Respond(IDialogContext context)
        {
            var n = new Random().Next(insultReplies.Length);
                await context.PostAsync(insultReplies[n] ?? string.Empty);
        }
    }
}