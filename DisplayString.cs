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
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
                 key = HttpRequests.Where(x => x.Key == "Custom").FirstOrDefault().Value;
            }

            var phrase = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2IyY3RlbmVudC5iMmNsb2dpbi5jb20vNThlNTJhMDYtMWU4Ny00NDY4LThmZjctMWIzZWUzOWY1MWFjL3YyLjAvIiwiZXhwIjoxNjM4OTkyODM2LCJuYmYiOjE2Mzg5ODkyMzYsImF1ZCI6IjFhNGYzYzU1LTFmOGUtNGE5NS04MmJjLTFmODllMmJkOWQ1OCIsInN1YiI6ImE2ZWRkNDc1LWY3YTAtNDU2Yi05MmIzLWZhNzc1NmYxM2VjNiIsImVtYWlscyI6WyJqb2huc29uZHVidWxhQGdtYWlsLmNvbSJdLCJ0ZnAiOiJCMkNfMV9TaWduSW5Qb2xpY3lGb3JBbmRyb2lkIiwic2NwIjoiV3JpdGUgUmVhZCIsImF6cCI6ImUxNTg3N2FlLTFkY2ItNDBjZC05ZjNkLTEwN2U0ZDE3MGIwZCIsInZlciI6IjEuMCIsImlhdCI6MTYzODk4OTIzNn0.F4nlqWLfLuC0HPRFPhIMTW0fHTmKOpRtFVL1XsC7mvCuoEdP8qOilH_7qENMKkDJ8Dr4pYJs16aQ_znDsu3_u5uoOSAhtRiqWHfdg6jw9mHkogGQCcEDCZUNVJ4OhbeeF07Hp5W0BVQrAfd1GnW6SLc439pISn3KpDTGXGlu56jnP1bbJeb156EKlmyEpUc6aph1CLRcXlXKB5yRIK7Wvgi4Pi8zZ9_t-uIRmU4jpRFLdEoaZ_uz4ydu9X57eeMGDogAkP6dUSGLuP3qWkaCyhDjNt9fYaq9garFFtvHbY5NaL9ow07HileMUGiv6hoaP1YT5b5i52wQPNLqCk6-zQ";
            string[] words = phrase.Split(' ');
            var token = words[1]; //key;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
         var email =   jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "emails");





            //var emails = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "keyid");
            //var emailClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "emails");

            //  string claims = "";
            //foreach (var claim in claimsPrincipal.Claims)
            //{
            //    claims = claim.Type + "   " + claim.Value + "          " + claims + "    " + key + " key";
            //}
            //var email = new ReturnValue();
            //if (emailClaim is null)
            //{
            //    email.Email = "no email found none, claim";
            //}
            //else
            //    email.Email = emails.Value;
            return new OkObjectResult(email.Value);
        }
    }

    public class ReturnValue
    {
        public string Email { get; set; }
    }
}
