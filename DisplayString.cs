using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App8Function
{
    public static class DisplayString
    {
        [FunctionName("DisplayString")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "DisplayString/{name}")] HttpRequest req,string name,
            ILogger log)
        {
            return new OkObjectResult(name);
        }
    }
}
