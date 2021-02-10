using System;
using System.IO;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace Testing
{
    public class Program
    {
        static void Main()
        {
            using (var db = new DanContext())
            {
                db.Database.EnsureCreated();
                var blog = new User {UserId = 1, Username = "Dan"};
                db.Users.Add(blog);
                db.SaveChanges();
            }

            using (var db = new DanContext())
            {
                var blogs = db.Users
                    .ToList();
                Console.WriteLine(blogs.Count);
            }
        }
    }
}