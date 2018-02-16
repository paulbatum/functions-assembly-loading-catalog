using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;

namespace ReferenceNewerJsonNet
{    
    public static class UseJObjectInResponse
    {
        /// <summary>
        /// BROKEN - this example does not work correctly because functions fails to recognize that the result object is a JObject because the project is referencing JSON.NET version 10.x
        /// </summary>
        [FunctionName("UseJObjectInResponseDirectly")]
        public static HttpResponseMessage UseJObjectInResponseDirectly([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"JSON.NET user code version: {typeof(JObject).Assembly.FullName}");
            var result = new JObject
            {
                {"message", "Hello, World" }
            };            

            return req.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// WORKING - this example works correctly because no exchange of types occurs
        /// </summary>
        [FunctionName("UseJObjectInResponseViaStringContent")]
        public static HttpResponseMessage UseJObjectInResponseViaStringContent([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"JSON.NET user code version: {typeof(JObject).Assembly.FullName}");
            var result = new JObject
            {
                {"message", "Hello, World" }
            };

            var response =  req.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(result.ToString(Newtonsoft.Json.Formatting.None), Encoding.UTF8, "application/json");
            return response;
        }
    }
}
