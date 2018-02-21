using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ReferenceNewerStorageSDK
{
    public static class BindToStringUseSDK
    {
        private static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
        private static CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

        /// <summary>
        /// Manually defined in Newtonsoft 11.0.1 for Storage 9.0.0
        /// Works and adds message to queue using storage SDK 9.0.0
        /// </summary>
        [Disable()]
        [FunctionName("BindToStringUseSDK")]
        public static async Task RunAsync([QueueTrigger("myqueue")]string myQueueMessage, TraceWriter log)
        {
                log.Info($"C# Queue trigger function processed: {myQueueMessage}");
                var message = queueClient.GetQueueReference("myotherqueue");
                await message.AddMessageAsync(new CloudQueueMessage(myQueueMessage));
            
        }

        /// <summary>
        /// Works same as above
        /// </summary>
        [Disable()]
        [FunctionName("BindToByteUseSDK")]
        public static async Task RunByte([QueueTrigger("myqueue")]byte[] myQueueMessage, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {Encoding.UTF8.GetString(myQueueMessage)}");
            var message = queueClient.GetQueueReference("myotherqueue");
            await message.AddMessageAsync(new CloudQueueMessage(myQueueMessage));

        }
    }
}
