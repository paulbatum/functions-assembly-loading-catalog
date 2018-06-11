using System;
using System.IO;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ReferenceMicrosoftDevices
{
    public static class BlobTrigger_CloudBlockBlob_Devices
    {
        // Put the connection string in IoTHubConnectionString in local.settings.json Values
        private static string connectionString = Environment.GetEnvironmentVariable("IoTHubConnectionString");
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

        [FunctionName("BlobTrigger_CloudBlockBlob_Devices")]
        public static async System.Threading.Tasks.Task RunAsync([BlobTrigger("sample/{name}", Connection = "AzureWebJobsStorage")]CloudBlockBlob myBlob, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{myBlob.Name}");

            await serviceClient.SendAsync("myDevice", new Message(await myBlob.OpenReadAsync()));
        }
    }
}
