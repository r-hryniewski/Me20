using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Me20.Bot.Services;
using System.Linq;
using System.Text;

namespace Me20.Bot.Dialogs
{
    [Serializable]
    public class TagsListDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await RespondAsync(context);
            context.Done(context.Activity);
        }

        private async Task RespondAsync(IDialogContext context)
        {
            try
            {
                //TODO: Extract and wrap this in something
                var tagsList = await new Me20Client().GetTagsListAsync();
                // return our reply to the user
                if (tagsList != null && tagsList.Any())
                {
                    await context.PostAsync($"I know about {tagsList.Length} tags now. Feel free to ask me about any of the following: {tagsList.OrderBy(x => x).Aggregate(seed: new StringBuilder(), func: (sb, tag) => sb.Append("'").Append(tag).Append("', "), resultSelector: sb => sb.ToString().TrimEnd(new char[] { ',', ' ' }))}. (The more I look at this list the more I think my creator should think about some kind of moderation)");
                }
                else
                {
                    //Action card with available tags?
                    await context.PostAsync($"Something went wrong and I cannot any available tags for you now. I'm pretty sure you'll find something about 'programming', 'coding' or 'DSP' (blog posts from Get Noticed contestants)");
                }
            }
            catch (Exception)
            {
                await context.PostAsync($"Something went wrong and I cannot fetch any available tags for you now. I'm pretty sure you'll find something about 'programming', 'coding' or 'DSP' (blog posts from Get Noticed contestants)");
            }

        }
    }
}