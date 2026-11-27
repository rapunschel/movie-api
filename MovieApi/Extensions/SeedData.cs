using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MovieApi.Models;

namespace MovieApi.Extensions;


public static class Extension
{
    public static void SeedData(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

        // Don't seed the database more than once
        if (context.Movies.Any())
            return;

        // 10 Genres
        var genres = new List<Genre>
        {
            new() { Name = "Action" },
            new() { Name = "Comedy" },
            new() { Name = "Drama" },
            new() { Name = "Horror" },
            new() { Name = "Science Fiction" },
            new() { Name = "Romance" },
            new() { Name = "Thriller" },
            new() { Name = "Adventure" },
            new() { Name = "Fantasy" },
            new() { Name = "Animation" }
        };

        // 10 Actors
        var actors = new List<Actor>
        {
            new() { Name = "Leonardo DiCaprio", BirthYear = 1974 },
            new() { Name = "Tom Hanks", BirthYear = 1956 },
            new() { Name = "Morgan Freeman", BirthYear = 1937 },
            new() { Name = "Scarlett Johansson", BirthYear = 1984 },
            new() { Name = "Brad Pitt", BirthYear = 1963 },
            new() { Name = "Natalie Portman", BirthYear = 1981 },
            new() { Name = "Christian Bale", BirthYear = 1974 },
            new() { Name = "Robert Downey Jr.", BirthYear = 1965 },
            new() { Name = "Emma Stone", BirthYear = 1988 },
            new() { Name = "Keanu Reeves", BirthYear = 1964 }
        };

        // 10 MovieDetails
        var details = new List<MovieDetails>
        {
            new() { Synopsis = "A thief enters people's dreams to steal secrets.", Language = "English", Budget = 160000000 },
            new() { Synopsis = "A man discovers that life is full of unexpected adventures.", Language = "English", Budget = 55000000 },
            new() { Synopsis = "A banker forms an unlikely friendship while in prison.", Language = "English", Budget = 25000000 },
            new() { Synopsis = "A superhero protects the world from a dangerous threat.", Language = "English", Budget = 200000000 },
            new() { Synopsis = "A group of survivors fight to stay alive in a dangerous world.", Language = "English", Budget = 90000000 },
            new() { Synopsis = "Two people fall in love despite their differences.", Language = "English", Budget = 40000000 },
            new() { Synopsis = "A detective investigates a mysterious crime.", Language = "English", Budget = 70000000 },
            new() { Synopsis = "An adventurer searches for a legendary treasure.", Language = "English", Budget = 120000000 },
            new() { Synopsis = "A young hero discovers a magical world.", Language = "English", Budget = 100000000 },
            new() { Synopsis = "A family discovers the importance of friendship and courage.", Language = "English", Budget = 80000000 }
        };

        // 10 Movies
        var movies = new List<Movie>
        {
            new() { Title = "Inception", Year = 2010, Duration = 148, Genre = genres[4], Details = details[0] },
            new() { Title = "Forrest Gump", Year = 1994, Duration = 142, Genre = genres[2], Details = details[1] },
            new() { Title = "The Shawshank Redemption", Year = 1994, Duration = 142, Genre = genres[2], Details = details[2] },
            new() { Title = "Iron Man", Year = 2008, Duration = 126, Genre = genres[0], Details = details[3] },
            new() { Title = "World War Z", Year = 2013, Duration = 116, Genre = genres[3], Details = details[4] },
            new() { Title = "La La Land", Year = 2016, Duration = 128, Genre = genres[5], Details = details[5] },
            new() { Title = "Se7en", Year = 1995, Duration = 127, Genre = genres[6], Details = details[6] },
            new() { Title = "Troy", Year = 2004, Duration = 163, Genre = genres[7], Details = details[7] },
            new() { Title = "Thor", Year = 2011, Duration = 115, Genre = genres[8], Details = details[8] },
            new() { Title = "Finding Nemo", Year = 2003, Duration = 100, Genre = genres[9], Details = details[9] }
        };

        // Connect actors to movies (many-to-many)
        movies[0].Actors.Add(actors[0]);
        movies[1].Actors.Add(actors[1]);
        movies[2].Actors.Add(actors[2]);
        movies[3].Actors.Add(actors[7]);
        movies[4].Actors.Add(actors[4]);
        movies[5].Actors.Add(actors[8]);
        movies[6].Actors.Add(actors[4]);
        movies[7].Actors.Add(actors[0]);
        movies[8].Actors.Add(actors[9]);
        movies[9].Actors.Add(actors[3]);

        // Add a second actor to each movie
        movies[0].Actors.Add(actors[5]);
        movies[1].Actors.Add(actors[8]);
        movies[2].Actors.Add(actors[1]);
        movies[3].Actors.Add(actors[9]);
        movies[4].Actors.Add(actors[6]);
        movies[5].Actors.Add(actors[3]);
        movies[6].Actors.Add(actors[2]);
        movies[7].Actors.Add(actors[6]);
        movies[8].Actors.Add(actors[7]);
        movies[9].Actors.Add(actors[5]);

        // 10 Reviews
        var reviews = new List<Review>
        {
            new() { ReviwerName = "Alex", Comment = "Amazing movie!", Rating = 5 },
            new() { ReviwerName = "Sarah", Comment = "Really enjoyed the story.", Rating = 4 },
            new() { ReviwerName = "John", Comment = "One of the best movies ever.", Rating = 5 },
            new() { ReviwerName = "Emma", Comment = "Great action scenes.", Rating = 4 },
            new() { ReviwerName = "David", Comment = "A little too scary for me.", Rating = 3 },
            new() { ReviwerName = "Lisa", Comment = "Beautiful cinematography.", Rating = 5 },
            new() { ReviwerName = "Michael", Comment = "Very suspenseful.", Rating = 4 },
            new() { ReviwerName = "Anna", Comment = "Excellent performances.", Rating = 5 },
            new() { ReviwerName = "James", Comment = "Really entertaining.", Rating = 4 },
            new() { ReviwerName = "Sophie", Comment = "A wonderful family movie.", Rating = 5 }
        };

        // Connect one review to each movie
        for (int i = 0; i < movies.Count; i++)
        {
            movies[i].Reviews.Add(reviews[i]);
        }

        // Save everything to the database
        context.Genres.AddRange(genres);
        context.Actors.AddRange(actors);
        context.Movies.AddRange(movies);

        context.SaveChanges();
    }
}
