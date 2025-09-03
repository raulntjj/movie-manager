using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
	private MovieContext _context;
	private IMapper _mapper;

	public MovieController(MovieContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}


	[HttpGet]
	public IEnumerable<Movie> getMovies()
	{
		return _context.Movies;
	}

	[HttpGet("pagination")]
	public IEnumerable<Movie> getMoviesPagination([FromQuery] int skip, [FromQuery] int take)
	{
		return _context.Movies.Skip(skip).Take(take);
	}

	[HttpGet("{id}")]
	public IActionResult getMovieById(int id)
	{
		var movie = _context.Movies.FirstOrDefault(filme => filme.Id == id);
		if (movie == null) return NotFound();
		return Ok(movie);
	}


	[HttpPost]
	public IActionResult AddMovie([FromBody] CreateMovieDTO movieDTO)
	{
		Movie movie = _mapper.Map<Movie>(movieDTO);
		_context.Movies.Add(movie);
		_context.SaveChanges();
		return CreatedAtAction(
			nameof(getMovieById),
			new { id = movie.Id },
			movie
		);
	}
}