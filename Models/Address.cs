using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace movies_api.Models;

public class Address
{
	[Key]
	[Required]
	public int Id { get; set; }

	[Required]
	public required string Street { get; set; }

	[Range(0, 99999)]
	public int Number { get; set; }

	[JsonIgnore]
	public virtual Cinema Cinema { get; set; }
}