using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class UpdateMovieDTO
{
	[Required]
	[StringLength(30)]
    public required string Title { get; set; }

	[Required]
	[StringLength(100)]
    public required string Genre { get; set; }

    [Required]
    [Range(70, 300)]
    public int Duration { get; set; }
}