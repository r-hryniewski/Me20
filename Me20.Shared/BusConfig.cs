using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared
{
    public class BusConfig
    {
        public static readonly string AzureServiceBusConnectionString = CloudConfigurationManager.GetSetting(nameof(AzureServiceBusConnectionString));
        public static readonly string BusEndpoint = CloudConfigurationManager.GetSetting(nameof(BusEndpoint));

        public static readonly string IdentityReadQueueName = "identity-read";
        public static readonly string IdentityWriteQueueName = "identity-write";
        public static readonly string ContentReadQueueName = "content-read";
        public static readonly string ContentWriteQueueName = "content-write";

        public static readonly Uri IdentityReadQueueUri = new Uri($"{BusEndpoint}{IdentityReadQueueName}");
        public static readonly Uri IdentityWriteQueueUri = new Uri($"{BusEndpoint}{IdentityWriteQueueName}");
        public static readonly Uri ContentReadQueueUri = new Uri($"{BusEndpoint}{ContentReadQueueName}");
        public static readonly Uri ContentWriteQueueUri = new Uri($"{BusEndpoint}{ContentWriteQueueName}");
    }
}
