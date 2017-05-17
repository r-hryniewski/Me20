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
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await Respond(context);
            context.Done(context.Activity);
        }

        public static async Task Respond(IDialogContext context)
        {
            
                await context.PostAsync($"Hello, I'm Me2.0 bot and once my brilliant creator place something here I'll explain how I work.");
        }
    }
}