using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
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
            var suc = jsonObject["event"]?.ToString();

            var hui = $"{suc}:{id}";

            res += hui;

            Program.SendNamedPipe(hui);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            Program.SendNamedPipe(res);
            return Ok(res);
        }
    }
}
