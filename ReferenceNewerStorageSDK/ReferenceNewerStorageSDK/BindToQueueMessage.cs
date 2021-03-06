using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ReferenceNewerStorageSDK
{
    public static class BindToQueueMessage
    {
        /// <summary>
        /// DOES NOT WORK - throws error on trigger as it is unable to serialize to this type.
        /// </summary>
        [Disable()]
        [FunctionName("BindToQueueMessage")]
        public static void Run([QueueTrigger("myqueue")]CloudQueueMessage myQueueMessage, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueMessage.AsString}");
        }
    }
}
