using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models;

public class Review
{
    public int Id { get; set; }
    public string ReviwerName { get; set; }
    public string Comment { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }
}
