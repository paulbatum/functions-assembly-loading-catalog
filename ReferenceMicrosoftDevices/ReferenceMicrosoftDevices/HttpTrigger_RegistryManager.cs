using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ReferenceMicrosoftDevices
{
    public static class Function1
    {
        // Put the connection string in IoTHubConnectionString in local.settings.json Values
        private static string connectionString = Environment.GetEnvironmentVariable("IoTHubConnectionString");
        private static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        [FunctionName("HttpTrigger_RegistryManager")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]Stream req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            IQuery query = registryManager.CreateQuery("SELECT * FROM devices", 10000);
            while (query.HasMoreResults)
            {
                var page = await query.GetNextAsTwinAsync();
                foreach (Twin twin in page)
                {
                    Console.WriteLine(twin.DeviceId);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}