using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Me20.Bot.Dialogs
{
    [ConfiguredLuisModel]
    [Serializable]
    public class LUISDialog : LuisDialog<object>
    {
        public LUISDialog()
        {
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //TODO: Help msg?
            await context.PostAsync($"I'm sorry I don't know what you mean. Can you explain it in different words? There is also possiblity I don't know words you've used yet.{Environment.NewLine}You can also ask for help and/or say hi to me!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("TagQuery")]
        public async Task TagQuery(IDialogContext context, LuisResult result)
        {
            var tag = result.Entities.Where(e => e.Type.Equals("Tag", StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Entity;

            int count = 0;
            var countEntity = result.Entities.Where(e => e.Type.Equals("builtin.number", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (countEntity != null && countEntity.Resolution.TryGetValue("value", out object countObj) && int.TryParse(countObj.ToString(), out int countInt))
                count = countInt;

            if (!string.IsNullOrWhiteSpace(tag))
                context.Call(new TagQueryDialog(tag, count), Callback);
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("TagsList")]
        public async Task TagsList(IDialogContext context, LuisResult result)
        {
            //TODO: Tag form
            context.Call(new TagsListDialog(), Callback);
        }

        [LuisIntent("Utilities.Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            //TODO: Help form
            context.Call(new HelpDialog(), Callback);
        }

        [LuisIntent("Insult")]
        public async Task Insult(IDialogContext context, LuisResult result)
        {
            //TODO: Help form
            context.Call(new InsultDialog(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }

    [Serializable]
    public sealed class ConfiguredLuisModelAttribute : LuisModelAttribute
    {
        public ConfiguredLuisModelAttribute()
            : base(modelID: ConfigurationManager.AppSettings["LUISModelId"], subscriptionKey: ConfigurationManager.AppSettings["LUISSubscriptionKeyKey"])
        {
        }
    }
}