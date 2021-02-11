using WeatherLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;

namespace DatabaseLibrary.SQL
{
    public class WeatherContext : DbContext
    {
        public DbSet<WeatherModel> Weathers { get; set; }
        
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<WeatherModel>()
                    .Property(e => e.Id)
                    .ValueGeneratedOnAdd();

            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=dan.db");
            }
    }
}