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
                var list = db.WeathersDays
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
                var weather = db.WeathersDays
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
                var weather = db.WeathersDays.Single(b => b.WeatherDayId == id);
                
                if(weather != null)
                {
                    db.WeathersDays.Remove(weather);
                    db.SaveChanges();
                }
            }
        }
        
        // /api/WeatherForecast/AddWeather
        [HttpPost]
        [Route("api/AddWeather")]
        public void PostWeather(string summary, int temp)
        {
            using (var db = new WeatherContext())
            {
                //db.Database.EnsureCreated();
                db.WeathersDays.Add(new WeatherDay
                {
                    Temperature = temp,
                    Date = DateTime.Now,
                    WeekDay = summary,
                    WeatherWeek = new WeatherWeek()
                    {
                        WeekId = 1
                    }
                });
                db.SaveChanges();
            }
        }
    }
}