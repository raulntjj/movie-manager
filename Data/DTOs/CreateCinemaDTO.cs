using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class CreateCinemaDTO
{
	[Required]
	public required string Name { get; set; }
	public int AddressId { get; set; }
}