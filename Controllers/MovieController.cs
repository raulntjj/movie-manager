using Microsoft.AspNetCore.Mvc;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
  	private static List<Movie> movies = new List<Movie>();
	private static int id = 1;

	[HttpGet]
	public IEnumerable<Movie> getMovies()
	{
		return movies;
	}

	[HttpGet("{id}")]
	public Movie? getMovieById(int id)
	{
		return movies.FirstOrDefault(filme => filme.Id == id);
	}

	[HttpPost]
	public void AddMovie([FromBody] Movie movie)
	{
		movie.Id = id++;
		movies.Add(movie);
	}
}