using System;

namespace LINQ_toObject
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop.Execution();

            #region Test Compare and Distinct with Product2
            //var products = new List<Product2>
            //{
            //new Product2 { ID = 1, ProductCode = "P1" },
            //new Product2 { ID = 2, ProductCode = "P2" },
            //new Product2 { ID = 3, ProductCode = "P3" }
            //};

            //int resultCoun1 = products.Select(s => s).Distinct().Count();
            //int resultCoun2 = products.Select(s => new {s.ID, s.ProductCode }).Distinct().Count(); 
            #endregion

        }
    }
}
