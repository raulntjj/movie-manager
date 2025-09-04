using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressController : ControllerBase
{
	private readonly MovieContext _context;
	private readonly IMapper _mapper;

	public AddressController(MovieContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IEnumerable<ReadAddressDTO> GetAddresses()
	{
		return _mapper.Map<List<ReadAddressDTO>>(_context.Addresses);
	}

	[HttpGet("pagination")]
	public IEnumerable<ReadAddressDTO> getAddressesPagination([FromQuery] int skip = 0, int take = 50)
	{
		return _mapper.Map<List<ReadAddressDTO>>(_context.Addresses.Skip(skip).Take(take));
	}

	[HttpGet("{id}")]
	public IActionResult GetAddressById(int id)
	{
		var address = _context.Addresses.FirstOrDefault(filme => filme.Id == id);
		if (address == null) return NotFound();
		
		var addressDTO = _mapper.Map<ReadAddressDTO>(address);

		return Ok(addressDTO);
	}


	[HttpPost]
	public IActionResult AddAddress([FromBody] CreateAddressDTO addressDTO)
	{
		Address address = _mapper.Map<Address>(addressDTO);
		_context.Addresses.Add(address);
		_context.SaveChanges();

		return CreatedAtAction(
			nameof(GetAddressById),
			new { id = address.Id },
			address
		);
	}

	[HttpPut("{id}")]
	public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDTO addressDTO)
	{
		var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

		if (address == null) return NotFound();

		_mapper.Map(addressDTO, address);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpPatch("{id}")]
	public IActionResult UpdatePartialAddress(int id, JsonPatchDocument<UpdateAddressDTO> patch)
	{
		var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

		if (address == null) return NotFound();

		var addressAtt = _mapper.Map<UpdateAddressDTO>(address);
		patch.ApplyTo(addressAtt, ModelState);

		if (!TryValidateModel(address)) return ValidationProblem(ModelState);

		_mapper.Map(addressAtt, address);
		_context.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteAddress(int id)
	{
		var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

		if (address == null) return NotFound();

		_context.Remove(address);
		_context.SaveChanges();
		return NoContent();
	}
}