using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OcelotSample.WebFront.Models;
using Steeltoe.Discovery.Client;

namespace OcelotSample.WebFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiscoveryClient discoClient;

        public HomeController(IDiscoveryClient discoClient)
        {
            this.discoClient = discoClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> API()
        {
            var apiGatewayInstances = discoClient.GetInstances("api-gateway");
            var apiGatewayUri = apiGatewayInstances.First().Uri;
            using (var client = new HttpClient())
            {
                // 调用计算服务，计算两个整数的和与差
                const int x = 124, y = 134;
                var sumResponse = await client.GetAsync($"{apiGatewayUri}calc/api/calc/add/{x}/{y}");
                sumResponse.EnsureSuccessStatusCode();
                var sumResult = await sumResponse.Content.ReadAsStringAsync();
                ViewData["sum"] = $"x + y = {sumResult}";

                var subResponse = await client.GetAsync($"{apiGatewayUri}calc/api/calc/sub/{x}/{y}");
                subResponse.EnsureSuccessStatusCode();
                var subResult = await subResponse.Content.ReadAsStringAsync();
                ViewData["sub"] = $"x - y = {subResult}";

                // 调用天气服务，计算大连和广州的平均气温标准差
                var stddevShenyangResponse = await client.GetAsync($"{apiGatewayUri}weather/api/weather/stddev/shenyang");
                stddevShenyangResponse.EnsureSuccessStatusCode();
                var stddevShenyangResult = await stddevShenyangResponse.Content.ReadAsStringAsync();
                ViewData["stddev_sy"] = $"沈阳：{stddevShenyangResult}";

                var stddevGuangzhouResponse = await client.GetAsync($"{apiGatewayUri}weather/api/weather/stddev/guangzhou");
                stddevGuangzhouResponse.EnsureSuccessStatusCode();
                var stddevGuangzhouResult = await stddevGuangzhouResponse.Content.ReadAsStringAsync();
                ViewData["stddev_gz"] = $"广州：{stddevGuangzhouResult}";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
