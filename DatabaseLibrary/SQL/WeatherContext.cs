using WeatherLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using WeatherLibrary.Model;

namespace DatabaseLibrary.SQL
{
    public class WeatherContext : DbContext
    {
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