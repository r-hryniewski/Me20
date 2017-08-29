using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Me20.Content.WriteService.CommandConsumers;
using Microsoft.Azure;
using Nito.AsyncEx;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.WriteService
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(MainAsync);
        }

        private static async Task MainAsync()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Application started");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            await ConfigureLogger();
            var bus = await ConfigureBus();
            await bus.StartAsync();

            await Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Application running.");
            while (true)
            {
                
            }
        }


        //TODO: Extract somewhere
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private static async Task ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .ApplicationInsightsEvents(CloudConfigurationManager.GetSetting("AppliactionInsigthsInstrumentationKey"))
                .WriteTo
                .Console()
                .CreateLogger();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Logger configured");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
#pragma warning restore CS1998

        //TODO: Extract somewhere
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private static async Task<IBusControl> ConfigureBus()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {


            var bus = Bus.Factory.CreateUsingAzureServiceBus(
                configure: cfg =>
                {
                    var host = cfg.Host(
                        connectionString: Shared.BusConfig.AzureServiceBusConnectionString,
                        configure: hostCfg =>
                        {
                            hostCfg.OperationTimeout = TimeSpan.FromSeconds(5);
                        });
                    cfg.ReceiveEndpoint(
                        host: host,
                        queueName: Shared.BusConfig.ContentWriteQueueName,
                        configure: ec =>
                        {
                            ec.Consumer<AddContentCommandConsumer>();
                        });
                });

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Bus configured");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            return bus;
        }
    }
}
