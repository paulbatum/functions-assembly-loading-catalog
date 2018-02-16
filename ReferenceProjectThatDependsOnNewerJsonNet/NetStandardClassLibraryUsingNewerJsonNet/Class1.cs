using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace NetStandardClassLibraryUsingNewerJsonNet
{
    public class Class1
    {
        public string MakeAValue()
        {
            var result = new JObject
            {
                {"message", "Hello, World" }
            };

            return (string)result["message"];
        }
    }
}
