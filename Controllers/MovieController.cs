using Microsoft.AspNetCore.Mvc;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
  	private static List<Movie> movies = new List<Movie>();
	private static int id = 1;

	[HttpGet]
	public IEnumerable<Movie> getMovies()
	{
		return movies;
	}

	[HttpGet("pagination")]
	public IEnumerable<Movie> getMoviesPagination([FromQuery] int skip, [FromQuery] int take)
	{
		return movies.Skip(skip).Take(take);
	}

	[HttpGet("{id}")]
	public IActionResult getMovieById(int id)
	{
		var movie = movies.FirstOrDefault(filme => filme.Id == id);
		if (movie == null) return NotFound();
		return Ok(movie);
	}


	[HttpPost]
	public IActionResult AddMovie([FromBody] Movie movie)
	{
		movie.Id = id++;
		movies.Add(movie);
		
		return CreatedAtAction(
			nameof(getMovieById),
			new { id = movie.Id },
			movie
		);
	}
}