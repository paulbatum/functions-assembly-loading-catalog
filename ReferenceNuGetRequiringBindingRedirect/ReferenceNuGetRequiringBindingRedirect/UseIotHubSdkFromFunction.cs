using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ReferenceNuGetRequiringBindingRedirect
{
    public static class Function1
    {
        /// <summary>
        /// BROKEN - will throw error: Could not load file or assembly 'Validation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=2fc06f0d701809a7' or one of its dependencies. The system cannot find the file specified.
        /// This occurs because this NuGet needs a binding redirect simply to load its "Validation" dependency. See the ConsoleApp project for an example of the binding redirect that needs to be added
        /// Note, ideally this test would use a different NuGet package to demonstate the issue, one that does not need a connection string.
        /// </summary>        
        [FunctionName("UseIotHubSdkFromFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var connectionString = Environment.GetEnvironmentVariable("IotHubConnectionString") ?? throw new Exception("No connection string specified");            
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(connectionString, Microsoft.Azure.Devices.Client.TransportType.Http1);

            string dataBuffer = "Hello world" + Guid.NewGuid().ToString();
            Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
            await deviceClient.SendEventAsync(eventMessage);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
