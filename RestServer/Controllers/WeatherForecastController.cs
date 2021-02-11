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
using Testing;

namespace RestServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        

        [HttpGet]
        [Route("api/WeatherAll")]
        [Produces("application/json")]
        public List<WeatherModel> GetAllWeather()
        {
            using (var db = new WeatherContext())
            {
                Console.WriteLine("Querying for a blog");
                var list = db.Weathers
                    .ToList();
                return list;
            }
        }
        
        [HttpGet]
        [Route("api/WeatherTest")]
        [Produces("application/json")]
        public WeatherModel GetLatestWeather()
        {
            using (var db = new WeatherContext())
            {
                Console.WriteLine("Querying for a blog");
                var weather = db.Weathers
                    .OrderBy(b => b.Id)
                    .First();
                return weather;
            }
        }
        
        [HttpGet]
        [Route("api/WeatherById")]
        [Produces("application/json")]
        public WeatherModel GetWeatherById([FromQuery] int id)
        {
            using (var db = new WeatherContext())
            {
                Console.WriteLine("Querying for a blog");
                var weather = db.Weathers
                    .FirstOrDefault(b => b.Id == id);
                return weather;
            }
        }
        
        [HttpGet]
        [Route("api/DeleteWeatherById")]
        [Produces("application/json")]
        public void DeleteWeatherByid([FromQuery] int id)
        {
            using (var db = new WeatherContext())
            {
                var weather = db.Weathers.SingleOrDefault(b => b.Id == id);

                if(weather != null)
                {
                    db.Weathers.Remove(weather);
                    db.SaveChanges();
                }
            }
        }

        [HttpPost]
        [Route("api/AddWeather")]
        public void PostWeather([FromForm] string summary, [FromQuery] int temp)
        {
            using (var db = new WeatherContext())
            {
                db.Database.EnsureCreated();
                db.Weathers.Add(new WeatherModel
                {
                    TemperatureC = temp,
                    Date = DateTime.Now,
                    Summary = summary
                });
                db.SaveChanges();
            }
        }
    }
}