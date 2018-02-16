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
        /// WORKING - This works because of the explicit Json.NET reference and the fact that there is no exchange of Json.NET types between the user code and the functions runtime
        /// Note that if you remove the explicit reference to Json.NET 10.0.3 from this project then it will stop compiling due to the version conflict
        /// </summary>
        [FunctionName("ReferenceProjectThatDependsOnNewerJsonNet")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var class1 = new Class1();
            return req.CreateResponse(HttpStatusCode.OK, class1.MakeAValue());
        }
    }
}
