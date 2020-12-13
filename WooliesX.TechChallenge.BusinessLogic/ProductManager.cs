using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooliesX.TechChallenge.Entities;
using WooliesX.TechChallenge.Services;

namespace WooliesX.TechChallenge.BusinessLogic
{
    public class ProductManager : IProductManager
    {
        private readonly IResourceService _resourceService;

        public ProductManager(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        /// <summary>
        /// Trolley Total Calculation calling /resource/trolleyCalculator api
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public decimal GetTrolleyTotal(TrolleyCalc req)
        {
            decimal result;             
            result = _resourceService.TrolleyCalculate(req);
            return result;
        }

        /// <summary>
        /// Trolley Total Calculation without calling /resource/trolleyCalculator api
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public decimal GetTrolleyTotalWithoutAPICall(TrolleyCalc req)
        {
            List<Dictionary<string, decimal>> productPrices = new List<Dictionary<string, decimal>>();

            foreach (var item in req.Products)
            {
                productPrices.Add(new Dictionary<string, decimal> { { item.Name, item.Price } });
            }

            List<Dictionary<string, int>> requiredQuantities = new List<Dictionary<string, int>>();

            foreach (var item in req.Quantities)
            {
                requiredQuantities.Add(new Dictionary<string, int> { { item.Name, item.Quantity } });
            }           


          int requiredQuantity = req.Quantities[0].Quantity;
            int specialQuantity = req.Specials[0].Quantities[0].Quantity;
            decimal specialTotalPrice = req.Specials[0].Total;
            decimal productPrice = req.Products[0].Price;

            if (requiredQuantity < specialQuantity)
            {
                return requiredQuantity * productPrice;
            }
            else
            {
                int multiples = requiredQuantity / specialQuantity;
                int reminder = requiredQuantity % specialQuantity;

                decimal priceWithSpecials = (multiples * specialTotalPrice) + (reminder * productPrice);

                decimal priceWithOutSpecials = requiredQuantity * productPrice;

                return priceWithSpecials < priceWithOutSpecials ? priceWithSpecials : priceWithOutSpecials;
            }

            return 0;
        }

        /// <summary>
        /// product list based on Sort Option
        /// </summary>
        /// <param name="sortValue"></param>
        /// <returns></returns>
        public List<Product> GetProducts(SortEnum sortValue)
        {
            List<Product> productList = null;
            
            if (sortValue == SortEnum.Recommended)
            {
                productList = GetSortedRecommendedProducts();
            }
            else
            {
                productList = ProductHelper.Sort(GetProducts(), sortValue);
            }

            return productList;
        }


        private List<Product> GetSortedRecommendedProducts()
        {
            var shopperHistory = _resourceService.GetShopperHistory();
            var productList = new List<Product>();

            if (shopperHistory != null && shopperHistory.Count > 0)
            {
                foreach (ShopperHistory shistory in shopperHistory)
                {
                    foreach (Product p in shistory.Products)
                    {
                        if (productList.Exists(x => x.Name == p.Name))
                        {
                            var productItem = productList.Where(x => x.Name == p.Name).Select(u => { u.Quantity += p.Quantity; return u; }).ToList();
                            productList[productList.FindIndex(ind => ind.Name == p.Name)] = productItem[0];                    

                        }
                        else
                        {
                            productList.Add(p);
                        }
                    }
                }
            }

            return productList.OrderByDescending(p => p.Quantity).ToList();

        }

        private List<Product> GetProducts()
        {
            return _resourceService.GetProducts();
        }













    }
}
