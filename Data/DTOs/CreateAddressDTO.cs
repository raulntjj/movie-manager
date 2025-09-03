using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class CreateAddressDTO
{
	[Required]
	public required string Street { get; set; }

	public int Number { get; set; }
}