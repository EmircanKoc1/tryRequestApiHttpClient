using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductHttpController : ControllerBase
    {
        HttpClient client;

        public ProductHttpController(HttpClient client)
        {
            this.client = client;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProducts()
        {
            var url = "https://localhost:44357/api/Product/GetProducts";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);

            return Ok(products);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var url = "https://localhost:44357/api/Product/AddProduct";
            var productJson = JsonConvert.SerializeObject(product);
            var content = new StringContent(productJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            return response.IsSuccessStatusCode ? Ok("Ürün eklendi") : BadRequest("Ürün eklenemedi");
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var url = "https://localhost:44357/api/Product/DeleteProduct?id=";
            var response = client.DeleteAsync($"{url}{id}");
            var content = await response.Result.Content.ReadAsStringAsync();
            var deleted = JsonConvert.DeserializeObject<bool>(content);
            return deleted ? Ok("Silme başarılı") : BadRequest("Başarısız");


        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var url = "https://localhost:44357/api/Product/UpdateProduct";
            var jsonProduct = JsonConvert.SerializeObject(product);
            var content = new StringContent(jsonProduct,Encoding.UTF8,"application/json");
            var response = await client.PutAsync(url, content);


            return response.IsSuccessStatusCode?Ok("İşlem Başarılı"):BadRequest("İşlem başarısız");
        }

    }
}
