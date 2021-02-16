using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using DatabaseLibrary.SQL;
using WeatherLibrary.Model;

namespace RestServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        // /api/WeatherForecast/WeatherAll
        [HttpGet]
        [Route("WeatherAll")]
        public List<WeatherDay> GetAllWeather()
        {
            using (var db = new WeatherContext())
            {
                Console.WriteLine("Querying for a blog");
                var list = db.Weathers
                    .ToList();
                return list;
            }
        }
        
        // /api/WeatherForecast/WeatherById/{id}
        [HttpGet]
        [Route("WeatherById/{id}")]
        public WeatherDay GetWeatherById(int id)
        {
            using (var db = new WeatherContext())
            {
                Console.WriteLine("Querying for a blog");
                var weather = db.Weathers
                    .FirstOrDefault(b => b.WeatherDayId == id);
                return weather;
            }
        }
        
        // /api/WeatherForecast/DeleteWeatherById/{id}
        [HttpDelete]
        [Route("DeleteWeatherById/{id}")]
        public void DeleteWeatherByid(int id)
        {
            using (var db = new WeatherContext())
            {
                var weather = db.Weathers.Single(b => b.WeatherDayId == id);
                
                if(weather != null)
                {
                    db.Weathers.Remove(weather);
                    db.SaveChanges();
                }
            }
        }
        
        // /api/WeatherForecast/AddWeather
        [HttpPost]
        [Route("api/AddWeather")]
        public void PostWeather([FromForm] string summary, [FromQuery] int temp)
        {
            using (var db = new WeatherContext())
            {
                db.Database.EnsureCreated();
                db.Weathers.Add(new WeatherDay
                {
                    Temperature = temp,
                    Date = DateTime.Now,
                    WeekDay = "Wednesday"
                });
                db.SaveChanges();
            }
        }
    }
}