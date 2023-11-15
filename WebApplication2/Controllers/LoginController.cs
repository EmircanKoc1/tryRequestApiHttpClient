using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var url = "https://localhost:44357/api/Auth/Login";
            var formData =  new MultipartFormDataContent();
            formData.Add(new StringContent(model.Username),"username");
            formData.Add(new StringContent(model.Password),"password");

            var response = await _httpClient.PostAsync(url, formData);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var token =  JsonConvert.DeserializeObject<Token>(content);
                return Ok(token);
            }
            else
            {
                return BadRequest("İşlem başarısız");
            }

        }

    }
}
