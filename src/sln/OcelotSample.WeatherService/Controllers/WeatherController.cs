using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace OcelotSample.WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // const string StdDevCalculationApiURI = "http://localhost:49814/api/calc/stddev";

        const string ServiceUri = "http://localhost:59495";

        static readonly Dictionary<string, List<Tuple<DateTime, float>>> weatherData;

        static WeatherController()
        {
            weatherData = JsonConvert.DeserializeObject<Dictionary<string, List<Tuple<DateTime, float>>>>(System.IO.File.ReadAllText("weather_data.json"));
        }

        [HttpGet("stddev/{city}")]
        public async Task<IActionResult> StdDevForCity(string city)
        {
            if (!weatherData.ContainsKey(city.ToUpper()))
            {
                return BadRequest($"城市'{city}'的气象数据不存在。");
            }

            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync($"{ServiceUri}/stddev", weatherData[city.ToUpper()].Select(x => x.Item2).ToArray());
                response.EnsureSuccessStatusCode();
                return Ok(Convert.ToDouble(await response.Content.ReadAsStringAsync()));
            }
        }
    }
}