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
    public class HelpDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await Respond(context);
            context.Done(context.Activity);
        }

        public static async Task Respond(IDialogContext context)
        {
            
                await context.PostAsync($"I'm still under development so I can't do much. For now you can ask me to give you some links on chosen topic ('I want to read about coding' or 'give me something about SQL'). I'll give you some of the newest links that I know of if they're tagged with this particular topic.");
            await context.PostAsync($"If you don't know what you can ask about, I'll tell you what tags I have in Me2.0 application currently, all it takes is ask ('what do you know?')");
            await context.PostAsync($"If everything else fails and I still don't know what you mean you could ask me about content in a safe way: 'tag: topic(SQL/C#/etc)' or 'list tags'");
        }
    }
}