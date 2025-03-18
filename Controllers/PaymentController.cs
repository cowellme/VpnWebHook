using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Text;
using VpnWebHook.WebHook;

namespace VpnWebHook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {

        private static string res = string.Empty;

        [HttpPost]
        public IActionResult TransactionResult(object root)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(root.ToString() ?? "");

            var id = jsonObject["object"]?["id"]?.ToString();
            var sum = jsonObject["object"]?["amount"]?["value"]?.ToString();
            var suc = jsonObject["event"]?.ToString();

            string data = $"{suc}:{id}:{sum}";
            SendPostBot(data);

            return Ok();
        }

        private async void SendPostBot(string data)
        {
            var url = "http://localhost:5253/";

            using (var client = new HttpClient())
            {
                var content = new StringContent(data, Encoding.UTF8, "text/plain");
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ответ от сервера: " + responseBody);
                }
                else
                {
                    Console.WriteLine("Ошибка: " + response.StatusCode);
                }
            }
        }

        [HttpGet]
        public IActionResult Get() { return Ok("Worked!"); }
    }
}
