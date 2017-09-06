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
    public class CreateTagCommandConsumer : IConsumer<ISubscribeToTagCommand>,
                                            IConsumer<ITagContentCommand>
    {
        private readonly ContentRepository contentRepository;
        private readonly TagRepository tagRepository;

        public CreateTagCommandConsumer(/*ContentRepository contentRepository, TagRepository tagRepository*/)
        {
            this.contentRepository = new ContentRepository()/*contentRepository*/;
            this.tagRepository = new TagRepository()/*tagRepository*/;
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

        public async Task Consume(ConsumeContext<ITagContentCommand> context)
        {
            var cmd = context.Message;
            var tag = CreateTagEntity(cmd);
            await PersistCreatedTag(tag);

            await contentRepository.AddContentTaggedWithEdgeAsync(cmd, tag);
        }

        private ITag CreateTagEntity(IHaveTagName tagNameContainer) => new TagEntity(tagNameContainer.TagName);

        private async Task PersistCreatedTag(ITag tag)
        {
            await tagRepository.AddTagVertexAsync(tag);
        }
    }
}
