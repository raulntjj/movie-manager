using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/cinemas")]
public class CinemaController : ControllerBase
{
	private readonly MovieContext _context;
	private readonly IMapper _mapper;

	public CinemaController(MovieContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IEnumerable<ReadCinemaDTO> getCinemas()
	{
		return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas);
	}

	[HttpGet("pagination")]
	public IEnumerable<ReadCinemaDTO> getCinemasPagination([FromQuery] int skip = 0, int take = 50)
	{
		return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.Skip(skip).Take(take));
	}

	[HttpGet("{id}")]
	public IActionResult getCinemaById(int id)
	{
		var cinema = _context.Cinemas.FirstOrDefault(filme => filme.Id == id);
		if (cinema == null) return NotFound();
		
		var cinemaDTO = _mapper.Map<ReadCinemaDTO>(cinema);

		return Ok(cinemaDTO);
	}


	[HttpPost]
	public IActionResult AddCinema([FromBody] CreateCinemaDTO cinemaDTO)
	{
		Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
		_context.Cinemas.Add(cinema);
		_context.SaveChanges();

		return CreatedAtAction(
			nameof(getCinemaById),
			new { id = cinema.Id },
			cinema
		);
	}

	[HttpPut("{id}")]
	public IActionResult updateCinema(int id, [FromBody] UpdateCinemaDTO cinemaDTO)
	{
		var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

		if (cinema == null) return NotFound();

		_mapper.Map(cinemaDTO, cinema);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpPatch("{id}")]
	public IActionResult updatePartialCinema(int id, JsonPatchDocument<UpdateCinemaDTO> patch)
	{
		var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

		if (cinema == null) return NotFound();

		var cinemaAtt = _mapper.Map<UpdateCinemaDTO>(cinema);
		patch.ApplyTo(cinemaAtt, ModelState);

		if (!TryValidateModel(cinema)) return ValidationProblem(ModelState);

		_mapper.Map(cinemaAtt, cinema);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult deleteCinema(int id)
	{
		var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

		if (cinema == null) return NotFound();

		_context.Remove(cinema);
		_context.SaveChanges();
		return NoContent();
	}
}