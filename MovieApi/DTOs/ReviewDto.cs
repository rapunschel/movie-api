using System;

namespace MovieApi.DTOs;

public class ReviewDto
{
    public int Id { get; set; }
    public string ReviwerName { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}
