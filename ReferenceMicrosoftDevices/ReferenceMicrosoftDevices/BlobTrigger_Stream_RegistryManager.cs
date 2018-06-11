using System;
using System.IO;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ReferenceMicrosoftDevices
{
    public static class BlobTrigger_Stream_RegistryManager
    {
        // Put the connection string in IoTHubConnectionString in local.settings.json Values
        private static string connectionString = Environment.GetEnvironmentVariable("IoTHubConnectionString");
        private static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        [FunctionName("BlobTrigger_Stream_RegistryManager")]
        public static async System.Threading.Tasks.Task RunAsync([BlobTrigger("sample/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name}");

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
