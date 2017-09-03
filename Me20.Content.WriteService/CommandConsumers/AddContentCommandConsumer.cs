using MassTransit;
using Me20.Content.Entities;
using Me20.Content.Repositories;
using Me20.Contracts.Commands;
using Me20.Contracts.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.WriteService.CommandConsumers
{
    public class AddContentCommandConsumer : IConsumer<IAddContentCommand>, IConsumer<IAddMyContentCommand>
    {
        private readonly ContentRepository repository;

        public AddContentCommandConsumer(/*ContentRepository repository*/)
        {
            this.repository = new ContentRepository()/*repository*/;
        }

        async Task IConsumer<IAddContentCommand>.Consume(ConsumeContext<IAddContentCommand> context)
        {
            await AddContent(context.Message);
            //Handle tags
        }

        async Task IConsumer<IAddMyContentCommand>.Consume(ConsumeContext<IAddMyContentCommand> context)
        {
            var cmd = context.Message;
            await AddContent(cmd);
            //Handle tags
            await PublishUserAddedContentEvent(context, cmd);
        }

        private static async Task PublishUserAddedContentEvent(ConsumeContext context, IAddMyContentCommand cmd)
        {
            await context.Publish<IContentAddedByUserEvent>(new
            {
                UserName = cmd.UserName,
                ContentUri = cmd.ContentUri
            });
        }

        private async Task AddContent(IAddContentCommand cmd)
        {
            var contentToAdd = new ContentEntity(
                uri: cmd.ContentUri,
                tags: cmd.Tags);

            await repository.AddContentVertexAsync(contentToAdd);
        }
    }
}
