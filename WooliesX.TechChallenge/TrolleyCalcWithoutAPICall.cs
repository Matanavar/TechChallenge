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
using System.Collections.Generic;

namespace WooliesX.TechChallenge
{
    public class TrolleyCalcWithoutAPICall
    {
        private readonly IProductManager _productManager;

        public TrolleyCalcWithoutAPICall(IProductManager productManager)
        {
            _productManager = productManager;
        }

        /// <summary>
        /// Azure Function to calculate the trolley total 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("TrolleyCalcWithoutAPI")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "trolleyCalcWithoutAPI")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var reqProduct = JsonConvert.DeserializeObject<TrolleyCalc>(requestBody);

                var responseMsg = _productManager.GetTrolleyTotalWithoutAPICall(reqProduct);
                return new OkObjectResult(responseMsg);
            }
            catch (JsonException ex)
            {
                var result = new ObjectResult(((Newtonsoft.Json.JsonReaderException)ex).Path + " : Invalid input");
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
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