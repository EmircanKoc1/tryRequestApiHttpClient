using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var url = "https://localhost:44357/api/Auth/Login";
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Username), "username");
            formData.Add(new StringContent(model.Password), "password");
            var response = await _httpClient.PostAsync(url, formData);
            if (!response.IsSuccessStatusCode)
            {
                RedirectToAction("Login", "Home");
            }

            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(content);
            AddCookie(token);

            RedirectToAction("Index", "Home");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var url = "https://localhost:44357/api/Product/GetProducts";
            if (ReadCookie() is null)
            {

                return View(new List<Product> { new Product { Price = 1, ProductID = 1, Stock = 1, ProductName = "adadad" } });
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ReadCookie());
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                RedirectToAction("Index", "Home");
            }

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);
            return View(products);

        }





        [NonAction]
        public void AddCookie(Token token)
        {
            var key = "AccessToken";
            var key2 = "RefreshToken";

            Response.Cookies.Append(key, token.AccessToken);
            Response.Cookies.Append(key2, token.RefreshToken);
        }
        [NonAction]
        public string ReadCookie()
        {

            return Request.Cookies["AccessToken"] is not null ? Request.Cookies["AccessToken"].ToString() : null;
        }
    }
}
