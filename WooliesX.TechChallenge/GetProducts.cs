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

        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sort")] HttpRequest req,
             ILogger log)
        {            
            string sortOption = req.Query["sortOption"];

            SortEnum sortVal = sortOption==null? SortEnum.High: (SortEnum)Enum.Parse(typeof(SortEnum), sortOption);

            var responseMessage = _productManager.GetProducts(sortVal);
            return new OkObjectResult(responseMessage);
        }
    }
}
