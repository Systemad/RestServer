using WeatherLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using WeatherLibrary.Model;

namespace DatabaseLibrary.SQL
{
    public class WeatherContext : DbContext
    {
        public WeatherContext()
        {
        }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }
        
        public DbSet<WeatherDay> Weathers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<WeatherDay>()
                    .Property(e => e.WeatherDayId)
                    .ValueGeneratedOnAdd();
                
                
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {   
                optionsBuilder.UseSqlite("Data Source=test.db");
            }
    }
}