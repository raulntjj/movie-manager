using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Cinema
{
	[Key]
	[Required]
	public int Id { get; set; }

	[Required]
	public required string Name { get; set; }
	
	public int AddressId { get; set; }

	public virtual Address? Address { get; set; }
}