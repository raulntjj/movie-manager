namespace movies_api.Data.DTOs;

public class ReadMovieDTO
{
	public required string Id { get; set; }
	public required string Title { get; set; }
	public required string Genre { get; set; }
	public int Duration { get; set; }
	public ICollection<ReadSessionDTO>? Sessions { get; set; }
	public DateTime QueryTimestamp { get; set; } = DateTime.Now;
}