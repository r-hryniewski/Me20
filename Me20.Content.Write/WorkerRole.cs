using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Serilog;
using System.Configuration;
using Me20.Content.Write.CommandConsumers;
using Microsoft.Azure;

namespace Me20.Content.Write
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("Me20.Content.Write is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .ApplicationInsightsEvents(CloudConfigurationManager.GetSetting("AppliactionInsigthsInstrumentationKey"))
                .CreateLogger();

            Trace.TraceInformation("Me20.Content.Write has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("Me20.Content.Write is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("Me20.Content.Write has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
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

            await bus.StartAsync();
            while (!cancellationToken.IsCancellationRequested)
            {
                
            }
            await bus.StopAsync();
        }
    }
}
