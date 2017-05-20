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
            
                await context.PostAsync($"Hello, it's nice to meet you. I'm Me2.0 bot and I'm companion app for my creator's pet project Me 2.0 available at https://me20.azurewebsites.net. You can also read some more about me at my creator's blog at http://hryniewski.net");
            await context.PostAsync($"I'm still learning but I'll understand most things you'll say to me, but in case you need help just ask for it, in case I don't understand that too just write 'HELP!'.");
        }
    }
}