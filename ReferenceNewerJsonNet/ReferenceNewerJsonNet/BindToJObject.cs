using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;

namespace ReferenceNewerJsonNet
{
    public static class BindToJObject
    {
        /// <summary>
        /// WORKING (TODO: get @fabiocav to explain how this works to me as I expected it was broken)
        /// </summary>        
        [FunctionName("BindToJObject")]
        public static string Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] JObject body, TraceWriter log)
        {
            log.Info($"JSON.NET user code version: {typeof(JObject).Assembly.FullName}");            
            return body.Properties().First().Value.ToString();
        }        
    }
}
