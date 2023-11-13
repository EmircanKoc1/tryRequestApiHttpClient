using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public static class ProductService
    {

        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                ProductID = 1,
                ProductName="PC",
                Price = 1,
                Stock = 1,
            },
             new Product
            {
                ProductID = 2,
                ProductName="Telefon",
                Price = 1,
                Stock = 1,
            },
              new Product
            {
                ProductID = 3,
                ProductName="Mouse",
                Price = 1,
                Stock = 1,
            },
               new Product
            {
                ProductID = 4,
                ProductName="Headohones",
                Price = 1,
                Stock = 1,
            },




        };
    }
}
