using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionController : ControllerBase
{
	private readonly MovieContext _context;
	private readonly IMapper _mapper;

	public SessionController(MovieContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IEnumerable<ReadSessionDTO> GetSessions()
	{
		return _mapper.Map<List<ReadSessionDTO>>(_context.Sessions.ToList());
	}

	[HttpGet("pagination")]
	public IEnumerable<ReadSessionDTO> GetSessionsPagination([FromQuery] int skip = 0, int take = 50)
	{
		return _mapper.Map<List<ReadSessionDTO>>(_context.Sessions.ToList().Skip(skip).Take(take));
	}

	[HttpGet("/movies/{movieId}/cinemas/{cinemaId}")]
	public IActionResult GetSessionById(int movieId, int cinemaId)
	{
		var session = _context.Sessions.FirstOrDefault(session => session.MovieId == movieId && session.CinemaId == cinemaId);
		if (session == null) return NotFound();

		var SessionDTO = _mapper.Map<ReadSessionDTO>(session);

		return Ok(SessionDTO);
	}

	[HttpPost]
	public IActionResult AddSession([FromBody] CreateSessionDTO sessionDTO)
	{
		Session session = _mapper.Map<Session>(sessionDTO);
		_context.Sessions.Add(session);
		_context.SaveChanges();

		return CreatedAtAction(
			nameof(GetSessionById),
			new { movieId = session.MovieId, cinemaId = session.CinemaId },
			session
		);
	}

	[HttpDelete("{movieId}/{cinemaId}")]
	public IActionResult DeleteSession(int movieId, int cinemaId)
	{
		var session = _context.Sessions.FirstOrDefault(session => session.MovieId == movieId && session.CinemaId == cinemaId);

		if (session == null) return NotFound();

		_context.Remove(session);
		_context.SaveChanges();
		return NoContent();
	}
}