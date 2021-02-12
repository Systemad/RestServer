using System;
using Microsoft.EntityFrameworkCore;
using WeatherLibrary.Model;

namespace DatabaseLibrary.SQL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Department
            modelBuilder.Entity<WeatherDay>().HasData(
                new WeatherDay()
                {
                    Date = DateTime.Now,
                    WeatherDayId = 1,
                    WeekDay = "Monday",
                    Temperature = 22,
                },

                new WeatherDay()
                {
                    Date = DateTime.Now,
                    WeatherDayId = 2,
                    WeekDay = "Tuesday",
                    Temperature = 18,
                },
                new WeatherDay()
                {
                    Date = DateTime.Now,
                    WeatherDayId = 3,
                    WeekDay = "Wednesday",
                    Temperature = 20,
                });

            modelBuilder.Entity<WeatherWeek>().
                HasData(
                    new WeatherWeek()
                    {
                        WeekId = 1,
                        WeekOfYear = 1,
                    },
                    new WeatherWeek()
                    {
                        WeekId = 2,
                        WeekOfYear = 2
                    }
            );
        }
    }
}