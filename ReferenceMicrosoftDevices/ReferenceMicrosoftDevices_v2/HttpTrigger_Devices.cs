using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using System.Text;

namespace ReferenceMicrosoftDevices_v2
{
    public static class HttpTrigger_Devices
    {
        // Put the connection string in IoTHubConnectionString in local.settings.json Values
        private static string connectionString = Environment.GetEnvironmentVariable("IoTHubConnectionString");
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

        [FunctionName("HttpTrigger_Devices")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            await serviceClient.SendAsync("myDevice", new Message(Encoding.UTF8.GetBytes("Hello IoT")));

            return new OkResult();
        }
    }
}
