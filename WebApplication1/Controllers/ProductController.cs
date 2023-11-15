using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(ProductService.Products);
        }

        [HttpPost("[action]")]
        public ActionResult<List<Product>> AddProduct(Product product)
        {
            ProductService.Products.Add(product);
            return Ok(ProductService.Products);
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteProduct(int id)
        {
            bool added = ProductService.Products.Remove(ProductService.Products.FirstOrDefault(x => x.ProductID == id));

            return Ok(added);
        }

        [HttpPut("[action]")]
        public ActionResult<List<Product>> UpdateProduct(Product product)
        {
            var _product = ProductService.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            return _product is not null ? Ok(_product.ProductName = "Güncellendi") : BadRequest("Güncellenemedi");

        }



    }
}
