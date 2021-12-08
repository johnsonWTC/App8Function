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
using Microsoft.AspNetCore.Authorization;

namespace App8Function
{
    public static class DisplayString
    {
        [Authorize]
        [FunctionName("DisplayString")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "App8Function/{name}")] HttpRequest req,
            ILogger log, ClaimsPrincipal claimsPrincipal)
        {


            var HttpRequests = req.Headers;
            var key = "";


            if (HttpRequests.ContainsKey("Custom"))
            {
                 key = HttpRequests.Where(x => x.Key == "Custom").ToString();
            }
            var emails = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "keyid");
            var emailClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "emails");


            string claims = "";
            foreach (var claim in claimsPrincipal.Claims)
            {
                claims = claim.Type + "   " + claim.Value + "          " + claims + "    " + key;
            }

            //var email = new ReturnValue();
            //if (emailClaim is null)
            //{
            //    email.Email = "no email found none, claim";
            //}
            //else
            //    email.Email = emails.Value;
            return new OkObjectResult(claims);
        }
    }

    public class ReturnValue
    {
        public string Email { get; set; }
    }
}
