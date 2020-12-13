using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.TechChallenge.Entities;
using WooliesX.TechChallenge.Services;
using Xunit;

namespace WooliesX.TechChallenge.BusinessLogic.Tests
{
    public class ProductManagerTests
    {
        [Fact]
        public void TrolleyTotal_Scenario()
        {
            decimal expectedResult = 18;
            var resourceServiceMock = new Mock<IResourceService>();
            resourceServiceMock.Setup(x => x.TrolleyCalculate(It.IsAny<TrolleyCalc>())).Returns(18);

            var trolleyCalc = getData();
            ProductManager pmgr = new ProductManager(resourceServiceMock.Object);
            decimal result = pmgr.GetTrolleyTotal(trolleyCalc);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TrolleyTotal_BadRequestScenario()
        {
            decimal expectedResult = 18;
            TrolleyCalc req = getData();
            req.Specials[0].Quantities[0].Quantity = 0;
            var resourceServiceMock = new Mock<IResourceService>();

           
            var trolleyCalc = getData();
            ProductManager pmgr = new ProductManager(resourceServiceMock.Object);
            decimal result = pmgr.GetTrolleyTotal(trolleyCalc);

          //  Assert.Throws<bad>(() => pmgr.

           Assert.Equal(expectedResult, result);
        }

        [Fact(DisplayName ="GetProducts_NullExceptionCase")]
        public void GetProducts_NullException()
        {
            var resourceServiceMock = new Mock<IResourceService>();
          
            var trolleyCalc = getData();
            ProductManager pmgr = new ProductManager(resourceServiceMock.Object);
            Assert.Throws<ArgumentNullException>(() => pmgr.GetProducts(SortEnum.Low));           
        }

        [Fact]
        public void GetProducts_SortOption_Low()
        {
            var resourceServiceMock = new Mock<IResourceService>();
            resourceServiceMock.Setup(x => x.GetProducts()).Returns(productsData);
                      
            ProductManager pmgr = new ProductManager(resourceServiceMock.Object);
            List<Product> actualList =  pmgr.GetProducts(SortEnum.Low);

            List<Product> expectedList = productsData().OrderBy(n => n.Price).ToList();
            Assert.Equal(3, actualList[0].Price);
        }

        [Fact(DisplayName = "GetProducts_SortOption_High", Skip = "To Do")]
        public void GetProducts_SortOption_High()
        {

        }

        [Fact(DisplayName = "GetProducts_Recommended", Skip = "To Do")]
        public void GetProducts_Recommended()
        {
             
        }

        

        public List<Product> productsData()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name="2",
                    Price= 6
                },
                 new Product
                {
                    Name="3",
                    Price=3
                },
                  new Product
                {
                    Name="4",
                    Price=7
                }

            };
        }

        public TrolleyCalc getData()
        {
            return new TrolleyCalc
            {
                Products = new System.Collections.Generic.List<Product>
                {
                    new Product
                    {
                        Name = "1",
                        Price = 3
                    }
                },
                Specials = new System.Collections.Generic.List<ProductSpecials>
                {
                    new ProductSpecials
                    {
                        Quantities = new System.Collections.Generic.List<Quantityy>
                        {
                             new Quantityy{
                                Name = "1",
                                Quantity = 2
                            }
                        },
                        Total = 5
                    }
                },
                Quantities = new System.Collections.Generic.List<Quantityy>
                {
                    new Quantityy
                    {
                        Name = "1",
                        Quantity = 7
                    }
                }
            };

        }

    }
}
