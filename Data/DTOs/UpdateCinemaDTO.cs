using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class UpdateCinemaDTO
{
	[Required]
	public required string Name { get; set; }
}