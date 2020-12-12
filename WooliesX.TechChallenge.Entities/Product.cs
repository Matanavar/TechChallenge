using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WooliesX.TechChallenge.Entities
{
    public class Product
    {
       

        public Product(string name, decimal price, long quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Product()
        {
        }

        public Product(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Quantity = product.Quantity;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }


    }

    public class SoldProduct: Product
    {
        public SoldProduct(Product product, long soldCount): base(product)
        {
            SoldCount = soldCount;
        }

        public long SoldCount { get; set; }

        public Product baseProduct()
        {
            return (Product)this;
        }
    }

    public static class ProductHelper
    {
        public static List<Product> Sort(List<Product> products, SortEnum sortVal)
        {
            List<Product> sortedProductList;
            if(products == null)
            {
                throw new ArgumentNullException("Not Valid");
            }
            switch (sortVal)
            {
                case SortEnum.Low:
                    sortedProductList = products.OrderBy(p => p.Price).ToList();
                    break;
                case SortEnum.High:
                    sortedProductList = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case SortEnum.Ascending:
                    sortedProductList = products.OrderBy(p => p.Name).ToList();
                    break;
                case SortEnum.Descending:
                    sortedProductList = products.OrderByDescending(p => p.Name).ToList();
                    break;

                default:
                    sortedProductList = products;
                    break;
            }
            return sortedProductList;

        }
    }


}
