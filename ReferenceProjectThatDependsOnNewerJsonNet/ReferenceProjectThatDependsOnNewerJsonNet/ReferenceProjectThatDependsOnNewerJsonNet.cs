using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using NetStandardClassLibraryUsingNewerJsonNet;

namespace ReferenceProjectThatDependsOnNewerJsonNet
{
    public static class ReferenceProjectThatDependsOnNewerJsonNet
    {
        /// <summary>        
        /// DOES NOT COMPILE due to version conflicts        
        /// </summary>
        [FunctionName("ReferenceProjectThatDependsOnNewerJsonNet")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var class1 = new Class1();
            return req.CreateResponse(HttpStatusCode.OK, class1.MakeAValue());
        }
    }
}
