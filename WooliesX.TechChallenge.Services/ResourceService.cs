using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WooliesX.TechChallenge.Entities;


namespace WooliesX.TechChallenge.Services
{
    public class ResourceService : IResourceService
    {
        private const string UrlPathBase = "api/resource/";
        private const string UrlPathProducts = UrlPathBase + "products";
        private const string UrlPathShopperHistory = UrlPathBase + "shopperHistory";
        private const string UrlPathTrolleyCalculator = UrlPathBase + "trolleyCalculator";

        private string resourceBaseUrl = Environment.GetEnvironmentVariable("ResourceBaseURL");
        private string resourceToken = Environment.GetEnvironmentVariable("ResourceToken");


        public List<Product> GetProducts()
        {
            string url = resourceBaseUrl + UrlPathProducts + "?token=" + resourceToken;            
            return HttpClientUtil.Get<List<Product>>(url).Result;
        }

        public List<ShopperHistory> GetShopperHistory()
        {
            return HttpClientUtil.Get<List<ShopperHistory>>(resourceBaseUrl + UrlPathShopperHistory + "?token=" + resourceToken).Result;
        }

        public decimal TrolleyCalculate(TrolleyCalc data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return HttpClientUtil.Post<decimal>(resourceBaseUrl + UrlPathTrolleyCalculator + "?token=" + resourceToken, content).Result;
        }
    }
}
