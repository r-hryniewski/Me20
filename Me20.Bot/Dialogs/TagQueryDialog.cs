using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Me20.Bot.Services;
using System.Linq;
using Microsoft.Bot.Builder.Luis.Models;

namespace Me20.Bot.Dialogs
{
    [Serializable]
    public class TagQueryDialog : IDialog<object>
    {

        readonly string tag;
        public TagQueryDialog(string _tag)
        {
            tag = _tag;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await RespondAsync(context);
            context.Done(context.Activity);
        }

        private async Task RespondAsync(IDialogContext context)
        {
            //TODO: Extract and wrap this in something
            // calculate something for us to return
            var contents = await new Me20Client().GetByTagAsync(tag);
            // return our reply to the user
            if (contents != null && contents.Any())
            {
                await context.PostAsync($"I've selected {contents.Count} links about {tag} for you. I hope you'll enjoy them!");
                var postsContentsTasks = contents.Select(c => context.PostAsync($"{c.Key}: {c.Value}")).ToArray();
                Task.WaitAll(postsContentsTasks, 10000);
            }
            else
            {
                //Action card with available tags?
                await context.PostAsync($"Sorry, I haven't found anything about {tag} :(");
            }
        }
    }
}