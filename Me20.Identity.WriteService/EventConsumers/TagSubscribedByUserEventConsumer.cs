using MassTransit;
using Me20.Contracts.Events;
using Me20.Identity.Repositories;
using Me20.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Identity.WriteService.EventConsumers
{
    public class TagSubscribedByUserEventConsumer : IConsumer<ITagSubscribedByUserEvent>
    {
        private readonly UserRepository repository;

        public TagSubscribedByUserEventConsumer(/*UserRepository repository*/)
        {
            this.repository = new UserRepository();//repository; 
        }

        public async Task Consume(ConsumeContext<ITagSubscribedByUserEvent> context)
        {
            var ev = context.Message;
            await repository.AddUserSubscribedTagEdgeAsync(
                userName: ev.UserName,
                tagNameContainer: ev);
        }
    }
}