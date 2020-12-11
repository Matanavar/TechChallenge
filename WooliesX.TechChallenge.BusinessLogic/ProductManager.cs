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

        public decimal GetTrolleyTotal(TrolleyCalc req)
        {
           return _resourceService.TrolleyCalculate(req);
        }

        /// <summary>
        /// Calculation without calling /resource/trolleyCalculator api
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public decimal GetTrolleyTotalWithoutAPICall(TrolleyCalc req)
        {
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
            var productList = new Dictionary<string, SoldProduct>();

            if (shopperHistory != null && shopperHistory.Count > 0)
            {
                foreach(ShopperHistory shistory in shopperHistory)
                {
                    foreach(Product p in shistory.Products)
                    {
                        if(productList.ContainsKey(p.Name))
                        {
                            productList[p.Name].Quantity += p.Quantity;
                            productList[p.Name].SoldCount++;
                        }
                        else
                        {
                            productList.Add(p.Name, new SoldProduct(p, 1));
                        }
                    }
                }
            }

            return productList.Values.ToList().OrderByDescending(p => p.SoldCount).ThenByDescending(p => p.Quantity)
                .Select(p => p.baseProduct()).ToList();
                
        }

        private List<Product> GetProducts()
        {
            return _resourceService.GetProducts();
        }


    }
}
