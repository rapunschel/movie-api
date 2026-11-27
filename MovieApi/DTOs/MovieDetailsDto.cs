using System;
using System.Collections;
using System.Collections.Generic;

namespace MovieApi.DTOs;

public class MovieDetailsDto
{
    public int Id { get; set; }
    public string Synopsis { get; set; }
    public string Language { get; set; }
    public double Budget { get; set; }

    List<ReviewDto> Reviews { get; set; }
    List<ActorDto> Actors { get; set; }

}
