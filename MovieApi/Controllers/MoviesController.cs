using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _context.Movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration
            }).ToListAsync();

            return Ok(movies);
        }

        // GET: api/Movies/5

        // GET: api/Movies/5
        // GET: api/Movies/5?withactors=true
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(
            int id,
            [FromQuery] bool withactors = false)
        {
            var query = _context.Movies.Include(m => m.Genre).AsQueryable();
            if (withactors)
            {
                query = query.Include(m => m.Actors);
            }

            var movie = await query.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration
            };

            if (withactors)
            {
                return Ok(new
                {
                    Movie = movieDto,
                    Actors = movie.Actors.Select(a => new ActorDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        BirthYear = a.BirthYear
                    }).ToList()
                });
            }

            return Ok(movieDto);
        }

        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var movie = await _context.Movies
                                .Include(m => m.Reviews)
                                .Include(m => m.Actors)
                                .Include(m => m.Details)
                                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            var details = new MovieDetailsDto
            {
                Id = movie.Details.Id,
                Synopsis = movie.Details.Synopsis,
                Language = movie.Details.Language,
                Budget = movie.Details.Budget,
                Reviews = movie.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviwerName = r.ReviwerName,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList(),

                Actors = movie.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                }).ToList()
            };
            return Ok(details);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
