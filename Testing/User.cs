using System;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Testing
{

    public class DanContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=dan.db");
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        
    }
}