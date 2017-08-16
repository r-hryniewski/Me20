using MassTransit;
using Me20.Contracts.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.Write.CommandConsumers
{
    public class AddContentCommandConsumer : IConsumer<IAddContentCommand>
    {
        public async Task Consume(ConsumeContext<IAddContentCommand> context)
        {
            var msg = context.Message;
            Log.Information($"IAddContentCommand received with {msg.ContentUri} url");
        }
    }
}
