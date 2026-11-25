

using System.Collections.Generic;
using System.Net;

namespace MovieApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }

    public Genre Genre { get; set; }
    public int Duration { get; set; }

    public MovieDetails Details { get; set; }

    public ICollection<Actor> Actors { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];


}
