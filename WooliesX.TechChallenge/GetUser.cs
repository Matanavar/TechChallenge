using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WooliesX.TechChallenge.BusinessLogic;

namespace WooliesX.TechChallenge
{
    public class GetUser
    {
        private readonly IUserManager _userManager;

        public GetUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Azure Function to return User details
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "answers/user")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var responseMessage = _userManager.GetUser();

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult("Error Occured");
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }
        }
    }
}
