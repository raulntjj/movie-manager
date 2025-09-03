using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class UpdateAddressDTO
{
	[Required]
	public required string Street { get; set; }

	public int Number { get; set; }
}