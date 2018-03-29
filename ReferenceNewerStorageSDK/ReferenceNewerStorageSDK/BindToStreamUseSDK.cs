using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ReferenceNewerStorageSDK
{
    public static class BindToStreamUseSDK
    {
        private static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
        private static CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

        /// <summary>
        /// DOES NOT WORK - throws error because "Stream" relies on Json.NET
        /// </summary>
        [Disable()]
        [FunctionName("BindToStreamUseSDK")]
        public static async Task RunAsync([QueueTrigger("myqueue")]Stream myQueueMessage, TraceWriter log)
        {
            using (StreamReader sr = new StreamReader(myQueueMessage))
            using (MemoryStream ms = new MemoryStream())
            {
                log.Info($"C# Queue trigger function processed: {sr.ReadToEnd()}");
                var message = queueClient.GetQueueReference("myotherqueue");
                myQueueMessage.CopyTo(ms);
                await message.AddMessageAsync(new CloudQueueMessage(ms.ToArray()));
            }
        }
    }
}
