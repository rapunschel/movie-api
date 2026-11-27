using System;
using System.ComponentModel.DataAnnotations;
using MovieApi.Models;

namespace MovieApi.DTOs;

public class MovieDto
{
    public int Id { get; set; }

    public string Title { get; set; }
    public int Year { get; set; }

    public Genre Genre { get; set; }
    public int Duration { get; set; }
}
