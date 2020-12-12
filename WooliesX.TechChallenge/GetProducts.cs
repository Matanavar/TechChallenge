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
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }
        /// <summary>
        /// Azure function to sort products based on sortOption
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sort")] HttpRequest req,
             ILogger log)
        {
            try
            {                 
                string sortOption = req.Query["sortOption"];

                SortEnum sortVal = sortOption == null ? SortEnum.High : (SortEnum)Enum.Parse(typeof(SortEnum), sortOption);

                var responseMessage = _productManager.GetProducts(sortVal);
                return new OkObjectResult(responseMessage);
            }            
            catch(Exception ex)
            {
                var result = new ObjectResult("Error Occured");
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }

        }
    }
}
