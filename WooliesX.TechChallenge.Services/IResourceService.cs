using System;
using System.Collections.Generic;
using System.Text;
using WooliesX.TechChallenge.Entities;

namespace WooliesX.TechChallenge.Services
{
    public interface IResourceService
    {
        List<Product> GetProducts();

        List<ShopperHistory> GetShopperHistory();

        decimal TrolleyCalculate(TrolleyCalc data);
    }
}
