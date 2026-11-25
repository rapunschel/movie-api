using System;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
namespace MovieApi;

public class MovieContext
{
    public class PubContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            Env.Load("/home/nelia/personal/lexicon-fullstack/api_fundamentals/db.env");
            var server = Environment.GetEnvironmentVariable("DB_SERVER");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            optionsBuilder
                .UseSqlServer($"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;");

        }
    }
}
