using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ReferenceMicrosoftDevices
{
    public static class HttpTrigger_Devices
    {
        // Put the connection string in IoTHubConnectionString in local.settings.json Values
        private static string connectionString = Environment.GetEnvironmentVariable("IoTHubConnectionString");
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

        [FunctionName("HttpTrigger_Devices")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            await serviceClient.SendAsync("myDevice", new Message(Encoding.UTF8.GetBytes("Hello IoT")));

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
