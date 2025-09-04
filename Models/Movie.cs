using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Movie
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public required string Title { get; set; }

    [Required]
    [StringLength(100)]
    public required string Genre { get; set; }

    [Required]
    [Range(70, 300)]
    public int Duration { get; set; }
    
    public virtual ICollection<Session>? Sessions { get; set; }
}