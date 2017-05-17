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
            await context.PostAsync("I'm sorry I don't know what you mean. TODO: Help msg?");
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
            if (!string.IsNullOrEmpty(tag))
                context.Call(new TagQueryDialog(tag), Callback);
            else
            {
                await context.PostAsync("I'm sorry I don't know what you mean. TODO: Help msg?");
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("TagsList")]
        public async Task TagsList(IDialogContext context, LuisResult result)
        {
            //TODO: Tag form
            context.Call(new TagsListDialog(), Callback);
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