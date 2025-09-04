using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/Sessions")]
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

	[HttpGet("{id}")]
	public IActionResult GetSessionById(int id)
	{
		var session = _context.Sessions.FirstOrDefault(filme => filme.Id == id);
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
			new { id = session.Id },
			session
		);
	}

	[HttpPut("{id}")]
	public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDTO SessionDTO)
	{
		var Session = _context.Sessions.FirstOrDefault(Session => Session.Id == id);

		if (Session == null) return NotFound();

		_mapper.Map(SessionDTO, Session);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteSession(int id)
	{
		var session = _context.Sessions.FirstOrDefault(Session => Session.Id == id);

		if (session == null) return NotFound();

		_context.Remove(session);
		_context.SaveChanges();
		return NoContent();
	}
}