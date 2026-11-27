using System;
using System.ComponentModel.DataAnnotations;
using MovieApi.Models;

namespace MovieApi.DTOs;

public class MovieUpdateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    [Required]
    public Genre Genre { get; set; }

    [Required]
    [Range(1, 999)]
    public int Duration { get; set; }
}
