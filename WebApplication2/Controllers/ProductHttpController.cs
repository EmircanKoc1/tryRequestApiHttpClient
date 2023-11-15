using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;
using System.Net.Http.Headers;
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
            var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRW1pciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNzAwMDc2OTk1LCJleHAiOjE3MDAwNzcwNTUsImlzcyI6Ind3dy5hcGkuY29tIiwiYXVkIjoidXNlcnMifQ.fmtpnbhNVkFGgFevIrV8lB1A8QWIG01kPF93-PWvorE";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(content);
                return Ok(products);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
               // var newToken = await GetRefreshToken(token.RefreshToken);
              //  await GetProducts();
            }
            return BadRequest("başarısız işlem");
        }
        [NonAction]
        public async Task<Token> GetRefreshToken(string refreshtoken)
        {
            var url = "https://localhost:44357/api/Auth/RefreshToken";
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(refreshtoken), "refreshToken");
            var response = await client.PostAsync(url, formData);
           if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(content);
                return token;

            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null ;
            }
            else
            {
                return null;
            }

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
            var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);


            return response.IsSuccessStatusCode ? Ok("İşlem Başarılı") : BadRequest("İşlem başarısız");
        }

    }
}
