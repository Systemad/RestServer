﻿using System;
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
        [Route("Weather")]
        [Produces("application/json")]
        public IEnumerable<WeatherModel>  GetWeather([FromQuery]string summary, int temp) 
        {
            var a = new WeatherModel();
            a.Date = DateTime.Now;
            a.Summary = summary;
            a.TemperatureC = temp;
            var list = new List<WeatherModel>();
            list.Add(a);
            return list;
        }


        [HttpPost]
        public void PostWeather([FromForm] string summary, [FromQuery] int temp)
        {
            using (var db = new WeatherContext())
            {
                db.Database.EnsureCreated();
                db.Weathers.Add(new WeatherModel
                {
                    //Id = 3,
                    TemperatureC = temp,
                    Date = DateTime.Now,
                    Summary = summary
                });
                //db.Weathers.Add(weather);
                db.SaveChanges();
            }
        }
    }
}