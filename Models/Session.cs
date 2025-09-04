using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Session
{
	[Key]
	[Required]
	public int Id { get; set; }

	[Required]
	public int MovieId { get; set; }

	[Required]
	public int CinemaId { get; set; }

	public virtual Movie? Movie { get; set; }
	public virtual Cinema? Cinema { get; set; }
	public virtual ICollection<Session>? Sessions { get; set; }
}