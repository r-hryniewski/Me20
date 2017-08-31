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
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            await Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Application started");

            await ConfigureLogger();
            var bus = await ConfigureBus();
            await bus.StartAsync();

            await Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Application running.");

            Console.ReadLine();
        }

        private static async Task ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .ApplicationInsightsEvents(CloudConfigurationManager.GetSetting("AppliactionInsigthsInstrumentationKey"))
                .WriteTo
                .Console()
                .CreateLogger();
            await Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Logger configured");
        }

        private static async Task<IBusControl> ConfigureBus()
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
            await Console.Out.WriteLineAsync($"{nameof(Me20.Content.WriteService)}: Bus configured");

            return bus;
        }
    }
}
