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
    public class UserAddedContentEventConsumer : IConsumer<IContentAddedByUserEvent>
    {
        private readonly ContentRepository repository;

        public UserAddedContentEventConsumer(/*ContentRepository repository*/)
        {
            this.repository = new ContentRepository();//repository; 
        }

        public async Task Consume(ConsumeContext<IContentAddedByUserEvent> context)
        {
            var ev = context.Message;
            await repository.AddContentAddedByUserEdgeAsync(ev.UserName, ev.ContentUri);
        }
    }
}
