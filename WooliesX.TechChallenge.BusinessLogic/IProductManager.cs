using System;
using System.Collections.Generic;
using System.Text;
using WooliesX.TechChallenge.Entities;

namespace WooliesX.TechChallenge.BusinessLogic
{
    public interface IProductManager
    {
        List<Product> GetProducts(SortEnum sortValue);
        decimal GetTrolleyTotal(TrolleyCalc req);
        decimal GetTrolleyTotalWithoutAPICall(TrolleyCalc req);
    }
}
