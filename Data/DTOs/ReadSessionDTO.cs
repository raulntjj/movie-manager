using movies_api.Models;

namespace movies_api.Data.DTOs;

public class ReadSessionDTO
{
	public int Id { get; set; }
	public ReadMovieDTO? Movie { get; set; }
	public ReadCinemaDTO? Cinema { get; set; }
}