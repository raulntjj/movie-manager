using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace movies_api.Models;

public class Cinema
{
	[Key]
	[Required]
	public int Id { get; set; }

	[Required]
	public required string Name { get; set; }
	
	[ForeignKey("Address")]
	public int AddressId { get; set; }

	[JsonIgnore]
	public virtual Address Address { get; set; }
}