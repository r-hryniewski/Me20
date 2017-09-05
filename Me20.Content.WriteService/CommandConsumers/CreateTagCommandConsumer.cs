using MassTransit;
using Me20.Content.Entities;
using Me20.Content.Repositories;
using Me20.Contracts;
using Me20.Contracts.Commands;
using Me20.Contracts.Entities;
using Me20.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.WriteService.CommandConsumers
{
    public class CreateTagCommandConsumer : IConsumer<ISubscribeToTagCommand>
    {
        private readonly TagRepository repository;

        public CreateTagCommandConsumer(/*TagRepository repository*/)
        {
            this.repository = new TagRepository()/*repository*/;
        }

        public async Task Consume(ConsumeContext<ISubscribeToTagCommand> context)
        {
            var cmd = context.Message;
            var tag = CreateTagEntity(cmd);
            await PersistCreatedTag(tag);
            await context.Publish<ITagSubscribedByUserEvent>(new
            {
                TagName = cmd.TagName,
                UserName = cmd.UserName
            });
        }

        private ITag CreateTagEntity(IHaveTagName tagNameContainer) => new TagEntity(tagNameContainer.TagName);

        private async Task PersistCreatedTag(ITag tag)
        {
            await repository.AddTagVertexAsync(tag);
        }
    }
}
