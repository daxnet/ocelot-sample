using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Steeltoe.Discovery.Client;

namespace OcelotSample.WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IDiscoveryClient discoClient;
        static readonly Dictionary<string, List<Tuple<DateTime, float>>> weatherData;

        static WeatherController()
        {
            weatherData = JsonConvert.DeserializeObject<Dictionary<string, List<Tuple<DateTime, float>>>>(System.IO.File.ReadAllText("weather_data.json"));
        }

        public WeatherController(IDiscoveryClient discoClient)
        {
            this.discoClient = discoClient;
        }

        [HttpGet("stddev/{city}")]
        public async Task<IActionResult> StdDevForCity(string city)
        {
            if (!weatherData.ContainsKey(city.ToUpper()))
            {
                return BadRequest($"城市'{city}'的气象数据不存在。");
            }

            var apiGatewayInstances = this.discoClient.GetInstances("api-gateway");
            var uri = apiGatewayInstances.First().Uri;
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync($"{uri}calc/api/calc/stddev", weatherData[city.ToUpper()].Select(x => x.Item2).ToArray());
                response.EnsureSuccessStatusCode();
                return Ok(Convert.ToDouble(await response.Content.ReadAsStringAsync()));
            }
        }
    }
}