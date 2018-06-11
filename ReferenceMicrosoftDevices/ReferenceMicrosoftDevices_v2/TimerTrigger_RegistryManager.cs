using System;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace ReferenceMicrosoftDevices_v2
{
    public static class TimerTrigger_RegistryManager
    {
        // Put the connection string in IoTHubConnectionString in local.settings.json Values
        private static string connectionString = Environment.GetEnvironmentVariable("IoTHubConnectionString");
        private static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        [FunctionName("TimerTrigger_RegistryManager")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            IQuery query = registryManager.CreateQuery("SELECT * FROM devices", 10000);
            while (query.HasMoreResults)
            {
                var page = await query.GetNextAsTwinAsync();
                foreach (Twin twin in page)
                {
                    Console.WriteLine(twin.DeviceId);
                }
            }

        }
    }
}
