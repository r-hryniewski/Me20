using MassTransit;
using Me20.Content.DAL.Entities;
using Me20.Content.DAL.Repositories;
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
        public async Task Consume(ConsumeContext<IAddContentCommand> context)
        {
            var cmd = context.Message;
            var repo = new ContentRepository();
            var contentToAdd = new ContentEntity(
                uri: cmd.ContentUri,
                tags: cmd.Tags);

            await repo.AddAsync(contentToAdd);
        }
    }
}
