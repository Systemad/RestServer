using System;
using WeatherLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using System.Linq;
using WeatherLibrary.Model;

namespace DatabaseLibrary.SQL
{
    public class WeatherContext : DbContext
    {
        public DbSet<WeatherWeek> WeatherWeeks { get; set; }
        public DbSet<WeatherDay> WeathersDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database.EnsureDeleted();
            /*
            modelBuilder.Entity<WeatherDay>()
                .Property(e => e.WeatherDayId)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<WeatherWeek>()
                .Property(e => e.WeekId)
                .ValueGeneratedOnAdd();
            */

            //modelBuilder.Entity<WeatherDay>()
            //    .HasOne(p => p.WeatherWeek)
            //    .WithMany(b => b.WeatherDays);

            modelBuilder.Entity<WeatherDay>()
                .HasKey(e => new { e.WeatherDayId, e.WeekId });
            
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=weatherdb.db");
            }
    }
}