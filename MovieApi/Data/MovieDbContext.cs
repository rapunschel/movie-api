using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
namespace MovieApi;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieDetails> MovieDetails { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Actor> Actors { get; set; }

}
