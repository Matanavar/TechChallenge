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
using WooliesX.TechChallenge.Entities;

namespace WooliesX.TechChallenge
{
    public class TrolleyTotal
    {
        private readonly IProductManager _productManager;

        public TrolleyTotal(IProductManager productManager)
        {
            _productManager = productManager;
        }

        /// <summary>
        /// Azure Function to return TrolleyTotal
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("TrolleyTotal")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var reqProduct = JsonConvert.DeserializeObject<TrolleyCalc>(requestBody);

                var responseMsg = _productManager.GetTrolleyTotal(reqProduct);
                return new OkObjectResult(responseMsg);
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
