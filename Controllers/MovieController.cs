using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
	private readonly MovieContext _context;
	private readonly IMapper _mapper;

	public MovieController(MovieContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}


	[HttpGet]
	public IEnumerable<ReadMovieDTO> getMovies()
	{
		return _mapper.Map<List<ReadMovieDTO>>(_context.Movies);
	}

	[HttpGet("pagination")]
	public IEnumerable<ReadMovieDTO> getMoviesPagination([FromQuery] int skip = 0, int take = 50)
	{
		return _mapper.Map<List<ReadMovieDTO>>(_context.Movies.Skip(skip).Take(take));
	}

	[HttpGet("{id}")]
	public IActionResult getMovieById(int id)
	{
		var movie = _context.Movies.FirstOrDefault(filme => filme.Id == id);
		if (movie == null) return NotFound();
		
		var movieDTO = _mapper.Map<ReadMovieDTO>(movie);

		return Ok(movieDTO);
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

	[HttpPut("{id}")]
	public IActionResult updateMovie(int id, [FromBody] UpdateMovieDTO movieDTO)
	{
		var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

		if (movie == null) return NotFound();

		_mapper.Map(movieDTO, movie);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpPatch("{id}")]
	public IActionResult updatePartialMovie(int id, JsonPatchDocument<UpdateMovieDTO> patch)
	{
		var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

		if (movie == null) return NotFound();

		var movieAtt = _mapper.Map<UpdateMovieDTO>(movie);
		patch.ApplyTo(movieAtt, ModelState);

		if (!TryValidateModel(movie)) return ValidationProblem(ModelState);

		_mapper.Map(movieAtt, movie);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult deleteMovie(int id)
	{
		var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

		if (movie == null) return NotFound();

		_context.Remove(movie);
		_context.SaveChanges();
		return NoContent();
	}
}