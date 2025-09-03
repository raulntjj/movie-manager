using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Movie
{
	[Required]
	[StringLength(30)]
    public required string Title { get; set; }

	[Required]
	[MaxLength(100)]
    public required string Genre { get; set; }

    [Required]
    [Range(70, 300)]
    public int Duration { get; set; }
}