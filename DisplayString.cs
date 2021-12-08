using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Linq;

namespace App8Function
{
    public static class DisplayString
    {
        [FunctionName("DisplayString")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ClaimsPrincipal claimsPrincipal)
        {
            var emails = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "scp");
            var emailClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "emails");
            var email = new ReturnValue();
            if (emails is null)
            {
                email.Email = "no email found none";
            }
            else
                email.Email = emails.Value;
            return new OkObjectResult(email);
        }
    }

    public class ReturnValue
    {
        public string Email { get; set; }
    }
}
