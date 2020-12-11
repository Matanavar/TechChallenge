using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesX.TechChallenge.Entities
{
    public class ShopperHistory
    {
        public long CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
