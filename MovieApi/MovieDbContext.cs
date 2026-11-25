using System;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using MovieApi.Models;
namespace MovieApi;

public class MovieDbContext : DbContext
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

    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieDetails> MovieDetails { get; set; }
    public DbSet<Genre> Genre { get; set; }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Actor> Actors { get; set; }

}
