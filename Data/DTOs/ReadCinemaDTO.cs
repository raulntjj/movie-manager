using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class ReadCinemaDTO
{
	public int Id { get; set; }
	public required string Name { get; set; }

	public ReadAddressDTO Address { get; set; }
	public DateTime QueryTimestamp { get; set; } = DateTime.Now;
}