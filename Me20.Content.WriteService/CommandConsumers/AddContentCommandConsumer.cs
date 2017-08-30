using MassTransit;
using Me20.Content.Entities;
using Me20.Content.Repositories;
using Me20.Contracts.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.WriteService.CommandConsumers
{
    public class AddContentCommandConsumer : IConsumer<IAddContentCommand>
    {
        private readonly ContentRepository repository;

        public AddContentCommandConsumer(/*ContentRepository repository*/)
        {
            this.repository = new ContentRepository()/*repository*/;
        }

        public async Task Consume(ConsumeContext<IAddContentCommand> context)
        {
            var cmd = context.Message;
            var contentToAdd = new ContentEntity(
                uri: cmd.ContentUri,
                tags: cmd.Tags);

            await repository.AddAsync(contentToAdd);
        }
    }
}
