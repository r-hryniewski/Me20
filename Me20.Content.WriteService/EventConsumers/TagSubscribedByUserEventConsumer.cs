using MassTransit;
using Me20.Content.Repositories;
using Me20.Contracts.Events;
using Me20.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.WriteService.EventConsumers
{
    public class TagSubscribedByUserEventConsumer : IConsumer<ITagSubscribedByUserEvent>
    {
        private readonly TagRepository repository;

        public TagSubscribedByUserEventConsumer(/*TagRepository repository*/)
        {
            this.repository = new TagRepository();//repository; 
        }

        public async Task Consume(ConsumeContext<ITagSubscribedByUserEvent> context)
        {
            var ev = context.Message;
            await repository.AddTagSubscribedByUserEdgeAsync(
                tagNameContainer: ev,
                userName: ev.UserName);
        }
    }
}