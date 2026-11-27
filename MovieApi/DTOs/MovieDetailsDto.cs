using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs;

public class MovieDetailsDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Synopsis { get; set; }
    [Required]

    public string Language { get; set; }
    [Required]
    public double Budget { get; set; }

    List<ReviewDto> Reviews { get; set; }
    List<ActorDto> Actors { get; set; }

}
